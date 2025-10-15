using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _progressBar;
    public Action OnNewGame;
    bool _isLoading = false;

    public async Task LoadScene(string scenename)
    {
        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenename);
        _isLoading = true;
        SceneTracker.Instance.Clear();
        await Task.Delay(1000);
        scene.allowSceneActivation = false;

        while (scene.progress < 0.9f)
        {
            await Task.Delay(10);
        }

        // Smoothly animate the last 10%
        float fakeProgress = 0f;
        while (fakeProgress < 1f)
        {
            fakeProgress += 0.02f;
            _progressBar.fillAmount = fakeProgress;
            await Task.Delay(10);
        }

        scene.allowSceneActivation = true;

        await Task.Delay(1000);

        _loadingCanvas?.gameObject.SetActive(false);
        _isLoading = false;
    }
    
    public async Task StartNewGameAsync()
    {
        _loadingCanvas.SetActive(true);
        Serializer.DeleteSave(SaveSlot.AutoSave);
        await LoadScene("GnurksCave"); // opening scene
        MainCamera.Instance.OnNewGame();
        OnNewGame?.Invoke();
        // await LoadScene("Shop"); 
    }

    public async Task LoadSaveAsync(SaveSlot slot) {
        if (_isLoading)
            return;

        PlayerSaveData save = Serializer.LoadFile<PlayerSaveData>("Player.json", slot);
        Serializer.SaveFile(save, "Player.json", SaveSlot.AutoSave);
        await LoadScene(save.CurrentScene);
    }

    public async void LoadSave(SaveSlot slot) => await LoadSaveAsync(slot);
    public async void StartNewGame() => await StartNewGameAsync();
    public async void MainMenu() {
        await LoadScene("MainMenu");
        Destroy(gameObject);
    }
}