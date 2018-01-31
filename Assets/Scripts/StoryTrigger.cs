using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Linq;

public class StoryTrigger : MonoBehaviour {
  
    public Rect windowR = new Rect(100,50,2000,1000);
    public bool triggered;
    public string followingPromptId = "s1";
    public string followingPrompt;
    XmlNodeList list;
    public string Text;
    public List<string> options;
    


	// Use this for initialization
	void Start () {
        // Position the window and set the story element to un triggered state
        windowR = new Rect((Screen.width/2)-200, 50, 400, 100);
        triggered = false;
        followingPromptId = "s1";


        string loadFrom = Application.dataPath + @"/Resources/Story.XML"; // ÅAth for loading the STORY xml
        XmlDocument doc = new XmlDocument();// XML DOCUMENT OBJECT
        string content = System.IO.File.ReadAllText(loadFrom); // Read the contents of the XML file

        doc.LoadXml(content);// Load the xml
        list = doc.GetElementsByTagName("prompt"); // Allthe promtp elements read.
        // COntainers for changing options ant text
        Text = "";
        options = new List<string>();
        
    }
	
// Event handling. Add buttons, their shapes and what they do
    void DoMyWindow(int windowID) {
        

        if (GUI.Button(new Rect(150, 30, 100, 20), options[0])) {
            Time.timeScale = 1; // Enable rendering
            triggered = false; // Set to untriggered statew
            // Clear text and options 
            Text = "";
            options = new List<string>();

        } else if (GUI.Button(new Rect(150, 60, 100, 20), options[1])) {
            Time.timeScale = 1;
            triggered = false;

            Text = "";
            options = new List<string>();
        }

    }



    // When the Story element gets triggered, Show the UI Window
   void OnGUI()
    {
        if (triggered) {
            
            windowR = GUI.Window(0, windowR, DoMyWindow, Text);
        }

    }
    // Set the element to triggered state and freeze the scene
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            FetchText(followingPromptId);
            triggered = true;
            Time.timeScale = 0; // scene freezes 
            

        }


        
    }



     void FetchText(string id) {

        XmlNodeList promptData;
        // Find the promt with right Id
        foreach (XmlNode data in list) {
            if (data.Attributes["id"].Value == id) {
               promptData = data.ChildNodes; // Read all of the children of the prompt element

                // Loop through all the children
                foreach (XmlNode text in promptData)
                {
                    // Fetch the next text
                    if (text.Name == "text") {
                        Text = text.InnerText;
                        




                    }
                    // Fetch button options
                    if (text.Name == "option") {
                        options.Add(text.InnerText);

                    }
                    // Fetch the next story element Id.
                    if (text.Name == "next") {
                        followingPromptId = text.InnerText;
                        break;
                    }




                }
                





            }


        }

       





    }

}
