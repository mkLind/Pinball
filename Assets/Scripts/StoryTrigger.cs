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
    public int fontSize;

    public string currentTask;
    public string currentCondition;
    public Texture paper;

	public Font myFont;

	// Use this for initialization
	void Start () {
        // Position the window and set the story element to un triggered state
        windowR = new Rect(0,0, Screen.width, 1000);
        triggered = false;
        active = true;
        followingPromptId = "s1";
        option1id = "s1";
        option2id = "s1";
        fontSize = 30;
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
		GUI.color = Color.black;
        GUI.Label(new Rect((Screen.width / 2) - 175, 100, 400, 300), Text); // DISplay the title as a label
        // Specify button. First dimensions and then text
        if (GUI.Button(new Rect((Screen.width/2) - 225, 330, 450, 50), options[0])) {

            Time.timeScale = 1; // Enable rendering
            triggered = false; // Set to untriggered state
            // Clear text and options 
            Text = "";
            options = new List<string>();
            // Set prompt id for following promt in addition to current task and current condition
            followingPromptId = optionId[0];
            currentTask = task[0];
            currentCondition = condition[0];
            // reset for next prompt fetch
            optionId = new List<string>();
            task = new List<string>();
            condition = new List<string>();
            // callback for sending the current task and condition to ValueContainer
            cont.SetTaskAndCond(currentTask, currentCondition);

        } else if (GUI.Button(new Rect((Screen.width / 2) - 225, 400, 450, 50), options[1])) {

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
            
            windowR = GUI.Window(0, windowR, DoMyWindow, paper);
            // Set font color and size
            GUI.color = Color.black;
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.button.fontSize = fontSize;
            GUI.skin.window.fontSize = fontSize;
			GUI.skin.font = myFont;
           
        }

    }
    // Set the element to triggered state and freeze the scene
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && !behav.taskStatus())
        {
            FetchText(followingPromptId);
            triggered = true;
            Time.timeScale = 0; // scene freezes 
            

        }


        
    }
    public void setEnabled() {

        active = true;
    }
    public void disable() {
     
       
        
        active = false;
    }
    public bool isEnabled() {
        return active;
    }
    public string getFollowingId() {
        return followingPromptId;
    }

    public void ActivateNextTask()
    {
        FetchText(followingPromptId);
        triggered = true;
        Time.timeScale = 0; // scene freezes 
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
