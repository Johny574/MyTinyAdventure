using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.U2D.Sprites;
using System.Linq;

public class SpriteSlicer : EditorWindow {
    static Vector2 _pivot = new Vector2(0.5f, 0.5f);
    static Vector2 _cellSize = new Vector2(32f, 32f);
    static SpriteDataProviderFactories _spriteFactory;
    static TextureImporter _textureImporter;
    void OnEnable() {
        _spriteFactory = new SpriteDataProviderFactories();
        _spriteFactory.Init();
    }

    void OnGUI() {
        _pivot = EditorGUILayout.Vector2Field("Pivot: ", _pivot);
        _cellSize = EditorGUILayout.Vector2Field("Cellsize: ", _cellSize);

        if (GUILayout.Button("Slice")) {
            Slice();
        }
        if (GUILayout.Button("Set Pivot")) {
            SetPivot();
        }

        this.Repaint();
    }

    [MenuItem("Tools/Sprite Tools/Sprite Slicer")]
    static void OpenSlicer() {
        GetWindow(typeof(SpriteSlicer));
    }

    static void Slice() {  
        UnityEngine.Object[] spriteSheets = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

        int maxX;
        int maxY;
        int divX;
        int divY;
        ISpriteEditorDataProvider _dataProviderHandle;

        for (int z = 0; z < spriteSheets.Length; z++) {
            var spritePath = AssetDatabase.GetAssetPath(spriteSheets[z]);            
            SetTextureImporter(spritePath);

            AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);

            _textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            Texture2D spriteSheet = spriteSheets[z] as Texture2D;

            maxY = spriteSheet.height;
            maxX = spriteSheet.width;
            divX = (int)MathF.Floor(maxX / _cellSize.x );
            divY = (int)MathF.Floor(maxY / _cellSize.y);

            _dataProviderHandle = _spriteFactory.GetSpriteEditorDataProviderFromObject(spriteSheet);
            _dataProviderHandle.InitSpriteEditorDataProvider();

            List<SpriteRect> spriteRects = new();            
            SpriteRect smd;

            for (int j = 0; j < divX; j++) {
                for (int i = 0; i < divY; i++) {
                    smd = new() {
                        pivot = _pivot,
                        name = spriteSheet.name + $"_{i}" + $"_{j}",
                        rect = new Rect(0 + (j * _cellSize.x), maxY - (i * _cellSize.y) - _cellSize.y, _cellSize.x, _cellSize.y)
                    };

                    if (HasSprite(smd.rect, spriteSheet)){
                        spriteRects.Add(smd);       
                    }
                }   
            }
            _dataProviderHandle.SetSpriteRects(spriteRects.ToArray());
            _dataProviderHandle.Apply();

            AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);
        }
    }


    static void SetTextureImporter(string spritePath) {
        _textureImporter = AssetImporter.GetAtPath(spritePath) as TextureImporter;   
        _textureImporter.isReadable = true;
    }

    void SetPivot() {

        UnityEngine.Object[] spriteSheets = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

        var factory = new SpriteDataProviderFactories();
        factory.Init();

        for (int z = 0; z < spriteSheets.Length; z++) {
            string spritePath = AssetDatabase.GetAssetPath(spriteSheets[z]);
            SetTextureImporter(spritePath);
            AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);

            Texture2D spriteSheet = spriteSheets[z] as Texture2D;

            var dataProvider = factory.GetSpriteEditorDataProviderFromObject(spriteSheet);
            dataProvider.InitSpriteEditorDataProvider();

    

            foreach (var smdhandle in dataProvider.GetSpriteRects()) {
                smdhandle.pivot = _pivot;
                _textureImporter.spritePivot = _pivot;       
            }
    
            dataProvider.SetSpriteRects(dataProvider.GetSpriteRects());
            dataProvider.Apply();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    
    // Checks if there is a sprite
    static bool HasSprite(Rect rect, Texture2D texture){
        bool hasSprite = false;        
        for (int x = 0; x < rect.width; x++) {
            for (int y = 0; y < rect.height; y++) {
                if (texture.GetPixel((int)rect.x + x, (int)rect.y + y).a > 0f){
                    hasSprite = true;
                    break;
                }
            }
        }

        return hasSprite;
    }
}