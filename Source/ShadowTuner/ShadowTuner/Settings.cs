using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShadowTuner
{
  [KSPAddon(KSPAddon.Startup.MainMenu, false)]
  public class ShadowTunerLoader : MonoBehaviour
  {

    public bool FirstLoad = true;

    public static ShadowTunerLoader Instance { get; private set; }

    protected void Awake()
    {
      Instance = this;
    }
    protected void Start()
    {
      Settings.Load();
    }
  }

  /// <summary>
  /// Class to load and hold configurable settings
  /// </summary>
  public static class Settings
  {

    public static float ShadowBias = 0.01f;
    public static float NormalBias = 0.58f;
    public static int ShadowProjectionMode = 0;
    public static int ShadowResolutionLevel = 4;

    public static void Load()
    {
      ConfigNode settingsNode;

      Debug.Log("[ShadowTuner][Settings]: Started loading");
      if (GameDatabase.Instance.ExistsConfigNode("ShadowTuner/ShadowTunerSettings"))
      {
        Debug.Log("[ShadowTuner][Settings]: Located settings file");
        settingsNode = GameDatabase.Instance.GetConfigNode("ShadowTuner/ShadowTunerSettings");
        settingsNode.TryGetValue("ShadowBias", ref ShadowBias);
        settingsNode.TryGetValue("NormalBias", ref NormalBias );
        settingsNode.TryGetValue("ShadowProjectionMode", ref ShadowProjectionMode);
        settingsNode.TryGetValue("ShadowResolutionLevel", ref ShadowResolutionLevel);
      }
      else
      {
        Debug.Log("[ShadowTuner][Settings]: Couldn't find settings file, using defaults");
      }
      Debug.Log("[ShadowTuner][Settings]: Finished loading");
    }
  }
}
