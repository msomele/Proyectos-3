using UnityEditor;
using UnityEngine;
using System.IO;

public class ScriptsCustomTool : EditorWindow
{

    /*TOOL BY SOMOZADEV <3
     * UR WELCOME
     * 
     * 
     * 
     */




    /////////NEEDS///////////////////
    string className = "";
    string savePath = "";
    string auxSavePath = "";
    bool wantsCustomSavePath = false;
    /////////BASICS///////////////////
    bool basicUsingUnity = true; 
    bool isMonoBehaviour = true;
    bool awakeFunction = false;
    bool startFunction = false;
    bool updateFunction = false;
    bool fixedUpdateFunction = false;
    /////////PHYSICS///////////////////
    bool hasCollisionsFunctions = false;
    bool isTrigger = false;
    bool isCollision = false;
    bool collisions2D = false;
    bool collisions3D = false;
    bool trigger2D = false;
    bool trigger3D = false;
    bool collision2D = false;
    bool collision3D = false;
    bool enter = false;
    bool stay = false;
    bool exit = false;
    string entryMethod = "";

    [MenuItem("Tools/ScriptsCustomTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ScriptsCustomTool));
        
    }
    private void OnGUI()
    {
        //////////////////////////////////NEEDS//////////////////////////////////////////////
        savePath = Application.dataPath;
        GUILayout.Label("Needs", EditorStyles.centeredGreyMiniLabel);
        className = EditorGUILayout.TextField("Class name", className);
        wantsCustomSavePath = EditorGUILayout.Toggle("Custom save path?", wantsCustomSavePath);
        if(wantsCustomSavePath)
        {
            auxSavePath = EditorGUILayout.TextField("Save path", auxSavePath);
        }

        //////////////////////////////////BASICS//////////////////////////////////////////////
        GUILayout.Label("Basics", EditorStyles.centeredGreyMiniLabel);
        basicUsingUnity = EditorGUILayout.Toggle("Unity basic namespaces", basicUsingUnity);
        isMonoBehaviour = EditorGUILayout.Toggle("MonoBehaviour", isMonoBehaviour);
        awakeFunction = EditorGUILayout.Toggle("Awake", awakeFunction);
        startFunction = EditorGUILayout.Toggle("Start", startFunction);
        updateFunction = EditorGUILayout.Toggle("Update", updateFunction);
        fixedUpdateFunction = EditorGUILayout.Toggle("FixedUpdate", fixedUpdateFunction);

        //////////////////////////////////PHYSICS//////////////////////////////////////////////

        GUILayout.Label("Physics", EditorStyles.centeredGreyMiniLabel);
        hasCollisionsFunctions = EditorGUILayout.Toggle("Physics checker collisions ", hasCollisionsFunctions);
        if(hasCollisionsFunctions)
        {
            isTrigger = EditorGUILayout.Toggle("Trigger", isTrigger); if (isTrigger)

            {
                trigger2D = EditorGUILayout.Toggle("Trigger 2D", trigger2D);
                trigger3D = EditorGUILayout.Toggle("Trigger 3D", trigger3D);
            }

            isCollision = EditorGUILayout.Toggle("Collision", isCollision);
                        
            if (isCollision)
            {
                collision2D = EditorGUILayout.Toggle("Collider 2D", collision2D);
                collision3D = EditorGUILayout.Toggle("Collider 3D", collision3D);
            }

            enter = EditorGUILayout.Toggle("Enter collision detection", enter);
            stay = EditorGUILayout.Toggle("Stay collision detection", stay);
            exit = EditorGUILayout.Toggle("Exit collision detection", exit);

        }


        if (GUILayout.Button("Create"))
        {
            auxSavePath = "/" + auxSavePath + "/";
            savePath += auxSavePath;
            className = char.ToUpper(className[0]) + className.Remove(0, 1);
            if (className == null) className = "AuxiliarClassName";
            savePath += className + ".cs";
            WriteCs();
            ResetTool();


        }

    }
    void ResetTool()
    {
        className = "";
        auxSavePath = "";
        wantsCustomSavePath = false;
        basicUsingUnity = true;
        isMonoBehaviour = true;
        awakeFunction = false;
        startFunction = false;
        updateFunction = false;
        fixedUpdateFunction = false;

        hasCollisionsFunctions = false;
        isTrigger = false;
        isCollision = false;
        collisions2D = false;
        collisions3D = false;
        trigger2D = false;
        trigger3D = false;
        collision2D = false;
        collision3D = false;
        enter = false;
        stay = false;
        exit = false;
    }
    void WriteCs()
    {
        using (StreamWriter writer = new StreamWriter(savePath))
        {
            AddLines(writer);
        }
    }

    void AddLines(StreamWriter writer)
    {
        
        if(basicUsingUnity)
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using System.Collections;");
            writer.WriteLine("using System.Collections.Generic;");
        }
        writer.WriteLine("");
        if (isMonoBehaviour)
        {
            writer.WriteLine("public class " + className + " : MonoBehaviour");
            writer.WriteLine("{");
            writer.WriteLine(" ");
        }
        else
        {
            writer.WriteLine("public class " + className);
            writer.WriteLine("{");
            writer.WriteLine(" ");
        }
        if(awakeFunction)
        {
            writer.WriteLine("  void Awake()");
            writer.WriteLine("  {");
            writer.WriteLine("  ");
            writer.WriteLine("  }");
        }
        writer.WriteLine("");
        if (startFunction)
        {
            writer.WriteLine("  void Start()");
            writer.WriteLine("  {");
            writer.WriteLine("  ");
            writer.WriteLine("  }");
        }
        if (updateFunction)
        {
            writer.WriteLine("  void Update()");
            writer.WriteLine("  {");
            writer.WriteLine("  ");
            writer.WriteLine("  }");
        }
        if (fixedUpdateFunction)
        {
            writer.WriteLine("  void FixedUpdate()");
            writer.WriteLine("  {");
            writer.WriteLine("  ");
            writer.WriteLine("  }");
        }
        if (enter)
            entryMethod = "Enter";
        PhysicsMethods(writer);
        if (stay)
            entryMethod = "Stay";
        PhysicsMethods(writer);
        if (exit)
            entryMethod = "Exit";
        PhysicsMethods(writer);








        writer.WriteLine("");
        writer.WriteLine("}");
    }


    void PhysicsMethods(StreamWriter writer)
    {
        if (trigger2D)
        {
            writer.WriteLine("  private void OnTrigger" + entryMethod + "2D(Collider2D col)");
            writer.WriteLine("  {");
            writer.WriteLine("   ");
            writer.WriteLine("  }");
        }
        if (trigger3D)
        {
            writer.WriteLine("  private void OnTrigger" + entryMethod + "(Collider col)");
            writer.WriteLine("  {");
            writer.WriteLine("   ");
            writer.WriteLine("  }");
        }
        if (collision2D)
        {
            writer.WriteLine("  private void OnCollision" + entryMethod + "2D(Collision2D col)");
            writer.WriteLine("  {");
            writer.WriteLine("   ");
            writer.WriteLine("  }");
        }
        if (collision3D)
        {
            writer.WriteLine("  private void OnCollision" + entryMethod + "(Collision col)");
            writer.WriteLine("  {");
            writer.WriteLine("   ");
            writer.WriteLine("  }");
        }
    }
    


}
