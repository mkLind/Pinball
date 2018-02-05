using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Linq;

public class StoryTrigger : MonoBehaviour {
  
    public Rect windowR = new Rect(100,50,2000,1000);
    public bool triggered;
    public bool active;
    public static string followingPromptId = "s1";
    public string option1id;
    public string option2id;
    public VariableContainer cont;
    public BallBehaviour behav;
    public string followingPrompt;
    XmlNodeList list;
    public string Text;
    public List<string> options;
    public List<string> optionId;
    public List<string> task;
    public List<string> condition;

    public string currentTask;
    public string currentCondition;
    


	// Use this for initialization
	void Start () {
        // Position the window and set the story element to un triggered state
        windowR = new Rect((Screen.width/2)-200, 50, 400, 100);
        triggered = false;
        active = true;
        followingPromptId = "s1";
        option1id = "s1";
        option2id = "s1";
     
        currentTask = "";
        currentCondition = "";
        cont = GameObject.Find("Table").GetComponent<VariableContainer>();
        behav = GameObject.Find("Ball").GetComponent<BallBehaviour>();


        string loadFrom = Application.dataPath + @"/Resources/Story.XML"; // ÅAth for loading the STORY xml
        XmlDocument doc = new XmlDocument();// XML DOCUMENT OBJECT
        string content = System.IO.File.ReadAllText(loadFrom); // Read the contents of the XML file

        doc.LoadXml(content);// Load the xml
        list = doc.GetElementsByTagName("prompt"); // Allthe promtp elements read.
        // COntainers for changing options ant text
        Text = "";
        options = new List<string>();
        optionId = new List<string>();

        task = new List<string>();
        condition = new List<string>();
    }

    public string getCurrentTask() {
        return currentTask;

    }

    public string getCurrentCondition()
    {
        return currentCondition;

    }

    // Event handling. Add buttons, their shapes and what they do
    void DoMyWindow(int windowID) {
        

        if (GUI.Button(new Rect(150, 30, 100, 20), options[0])) {

            Time.timeScale = 1; // Enable rendering
            triggered = false; // Set to untriggered state
            // Clear text and options 
            Text = "";
            options = new List<string>();

            followingPromptId = optionId[0];
            currentTask = task[0];
            currentCondition = condition[0];

            optionId = new List<string>();
            task = new List<string>();
            condition = new List<string>();

            cont.SetTaskAndCond(currentTask, currentCondition);

        } else if (GUI.Button(new Rect(150, 60, 100, 20), options[1])) {

            Time.timeScale = 1;
            triggered = false;

            followingPromptId = optionId[1];
            currentTask = task[1];
            currentCondition = condition[1];

            Text = "";
            options = new List<string>();
            optionId = new List<string>();
            task = new List<string>();
            condition = new List<string>();
            cont.SetTaskAndCond(currentTask, currentCondition);
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
        if (other.gameObject.CompareTag("Ball") && !behav.taskStatus())
        {
            FetchText(followingPromptId);
            triggered = true;
            Time.timeScale = 0; // scene freezes 
            

        }


        
    }
    public void setEnabled() {

        GameObject.Find("StoryElement").GetComponent<Renderer>().material.color = Color.white;
        active = true;
    }
    public void disable() {
        GameObject.Find("StoryElement").GetComponent<Renderer>().material.color = Color.black;
        
        active = false;
    }
    public bool isEnabled() {
        return active;
    }
    public string getFollowingId() {
        return followingPromptId;
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
                        options.Add(text.Attributes["text"].Value);
                        XmlNode next;
                        next = text.FirstChild; // next data
                        optionId.Add(next.InnerText);
                        

                        task.Add(next.Attributes["task"].Value);
                        condition.Add(next.Attributes["cond"].Value);

                       
               


                    }
               




                }
                





            }


        }

       





    }

}
