



using System;
using FletcherLibraries;
using UnityEngine;

public class ShopEvents : Singleton<ShopEvents>
{
    public Action<ShopComponent, GameObject> Open;
    public Action Close;
}