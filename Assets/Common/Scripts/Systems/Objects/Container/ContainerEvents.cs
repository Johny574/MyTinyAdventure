using System;
using FletcherLibraries;

public class ContainerEvents: Singleton<ContainerEvents> 
{
    public Action<Container> Open;
    public Action Close;
}