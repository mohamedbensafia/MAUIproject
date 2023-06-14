global using MyReference.View;
global using MyReference.ViewModel;
global using MyReference.Model;
global using MyReference.Services;


global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;


global using System.Data;

global using System.Diagnostics;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Runtime.CompilerServices;
global using System.Text.Json;
global using System.Management;


public class Globals
{
    public static List<Dbzanime> MyStaticList = new();
    public static Queue<string> SerialBuffer = new();
    internal static DataSet UserSets = new();
    public static User IdentifiedUser;
    public static Boolean identified;
}