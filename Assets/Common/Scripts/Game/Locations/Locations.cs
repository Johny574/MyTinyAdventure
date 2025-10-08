using System;
using System.Collections.Generic;
using System.Linq;

public static class Locations
{
    public static string[] Scenes = new string[22] {
        "GnurksCave",
        "Shop",
        "Forest1-1",
        "Forest1-2",
        "Forest1-3",
        "Forest1-4",
        "Forest1-5",
        "Desert2-1",
        "Desert2-2",
        "Desert2-3",
        "Desert2-4",
        "Desert2-5",
        "Swamp3-1",
        "Swamp3-2",
        "Swamp3-3",
        "Swamp3-4",
        "Swamp3-5",
        "Tundra4-1",
        "Tundra4-2",
        "Tundra4-3",
        "Tundra4-4",
        "Tundra4-5"
    };


    static string[] _towns = new string[1] {
        "Forest1-1",
    };

    public static string SearchPath(string origin, string destination) {
        int o = Array.IndexOf(Scenes, origin);
        int d = Array.IndexOf(Scenes, destination);

        return 0 < d ? Scenes[o + 1] : Scenes[o - 1];
    }

    public static string GetLastTown(int currentScene) {
        List<int> towns = _towns.Select(x => Array.BinarySearch(Scenes, x)).ToList();
        return Scenes[towns.Where(x => x <= currentScene).Max()];
    }

    //******** USE BINARARY SEARCH OR HASH SETS IF THE SCENES GROW TO BIG *********///

    // static string[] _towns = new string[1] {
    //     "Forest1-1",
    // };

    // public static string SearchPath(string origin, string destination)
    // {
    //     Scenes = Sort(Scenes);                     // sorting algorithm here
    //     int o = Array.BinarySearch(Scenes, origin);
    //     int d = Array.BinarySearch(Scenes, destination);

    //     return 0 < d ? Scenes[o + 1] : Scenes[o - 1];
    // }

    // public static string GetLastTown(int currentScene)
    // {
    //     List<int> towns = _towns.Select(x => Array.BinarySearch(Scenes, x)).ToList();
    //     return Scenes[towns.Where(x => x <= currentScene).Max()];
    // }

    public static int Index(string scene) => Array.IndexOf(Scenes, scene);
}