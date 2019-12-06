using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowTuner
{

  [KSPAddon(KSPAddon.Startup.Flight, false)]
  public class ShadowTuner : MonoBehaviour
  {

    protected Light keyLight;
    protected bool showUI = false;

    // GUI VARS
    private int mainWindowID = new System.Random(3256231).Next();
    private Rect windowPos = new Rect(200f, 200f, 500f, 200f);

    protected void Start()
    {

      windowPos = new Rect(200f, 200f, 610f, 315f);
      keyLight = FindLight();
      ApplySettings(keyLight);
      
    }
    protected void ApplySettings(Light l)
    {
      l.shadowBias = Settings.ShadowBias;
      l.shadowNormalBias = Settings.NormalBias;
    }


    public Light FindLight()
    {
      Light light = null;
      Light[] allLights = GameObject.FindObjectsOfType<Light>();
      foreach (Light l in allLights)
      {
        if (l.type == LightType.Directional)
        {
          Debug.Log(String.Format("[ShadowTuner] Found directional light {0}", l.name));
          if (l.name == "SunLight")
            light = l;
        }
      }
      Debug.Log(String.Format("[ShadowTuner] Using directional light {0}", light.name));

      DebugLight(light);
      return light;
    }
    public void DebugLight(Light l)
    {
      Debug.Log(String.Format("[ShadowTuner] Dumping Light Shadow Properties:\n" +
        "-Bias {0}\n -Near Plane{1}\n -Normal Bias {2}\n -Resolution {3}\n -Cast mode {4}",
        l.shadowBias, l.shadowNearPlane, l.shadowNormalBias, l.shadowResolution.ToString(), l.lightShadowCasterMode.ToString()));
    }
    public void Update()
    {
      if (keyLight != null)
      {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
          showUI = !showUI;
        }
      }
    }

    public void OnGUI()
    {
      if (Event.current.type == EventType.Repaint || Event.current.isMouse)
      {
      }
      if (showUI)
        UIDraw();
    }

    public void UIDraw()
    {
      GUI.skin = HighLogic.Skin;

      windowPos = GUI.Window(mainWindowID, windowPos, TestWindow, "");
    }

    private void TestWindow(int windowId)
    {
      if (GUILayout.Button("Detect Light"))
      {
        keyLight = FindLight();
      }
      GUILayout.BeginHorizontal();
      GUILayout.Label("Shadow Bias");
      keyLight.shadowBias = GUILayout.HorizontalSlider(keyLight.shadowBias, 0f, 2f, GUILayout.MinWidth(75f));
      GUILayout.Label(String.Format("{0:F2}", keyLight.shadowBias));
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      GUILayout.Label("Shadow Normal Bias");
      keyLight.shadowNormalBias = GUILayout.HorizontalSlider(keyLight.shadowNormalBias, 0f, 3f, GUILayout.MinWidth(75f));
      GUILayout.Label(String.Format("{0:F2}", keyLight.shadowNormalBias));
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      GUILayout.Label("Shadow Near Plane");
      keyLight.shadowNearPlane = GUILayout.HorizontalSlider(keyLight.shadowNearPlane, 0f, 10f, GUILayout.MinWidth(75f));
      GUILayout.Label(String.Format("{0:F2}", keyLight.shadowNearPlane));
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      GUILayout.Label("Shadow Resolution");
      keyLight.shadowResolution = (UnityEngine.Rendering.LightShadowResolution)(int)GUILayout.HorizontalSlider((int)keyLight.shadowResolution, 0f, 3f, GUILayout.MinWidth(75f));
      GUILayout.Label(String.Format("{0:F2}", keyLight.shadowResolution.ToString()));
      GUILayout.EndHorizontal();

      GUI.DragWindow();
    }
  }
}
