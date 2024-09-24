using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using UnityEngine.SceneManagement;

public delegate void action();

public class ColobDialogueSys : MonoBehaviour
{
    public GameObject dialogueWindow;
    public GameObject answers;
    public TMP_Text message;
    public TMP_Text answer;
    public Camera cam;
    public GameObject player;
    public GameObject kisss;

    Dictionary<string, action> actions = new Dictionary<string, action>();

    CDialogue dialogue = new CDialogue();
    public void loadDialogue(Object xmlFile)
    {
        dialogue.Clear();
        actions.Clear();
        actions.Add("dialogue end", dialogueEnd);
        actions.Add("game end", gameEnd);
        actions.Add("kiss", kiss);
        actions.Add("none", null);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.ToString());
        XmlNode messages = xmlDoc.SelectSingleNode("//messages");
        XmlNodeList messageNodes = xmlDoc.SelectNodes("//messages/message");

        foreach (XmlNode messageNode in messageNodes)
        {
            CMessage msg = new CMessage();
            msg.text = messageNode.ChildNodes[0].InnerText;
            msg.msgID = long.Parse(messageNode.Attributes["uid"].Value);
            dialogue.loadMessage(msg);

            foreach (XmlNode answerNode in messageNode.ChildNodes[1].ChildNodes)
            {
                CAnswer answ = new CAnswer();
                answ.answID = long.Parse(answerNode.Attributes["auid"].Value);
                answ.msgID = long.Parse(answerNode.Attributes["muid"].Value);
                answ.action = answerNode.Attributes["action"].Value;
                answ.text = answerNode.InnerText;
                dialogue.loadAnswer(answ);
            }
        }
        showMessage(dialogue.getMessages()[0].msgID, "none");
        dialogueWindow.SetActive(true);
    }
    public void showMessage(long uid, string act)
    {
        actions[act]?.Invoke();
        if (uid == -1) return;
        foreach (Transform child in answers.transform)
            Destroy(child.gameObject);

        message.text = dialogue.selectMessage(uid);

        foreach (CAnswer ans in dialogue.getAnswers())
        {
            TMP_Text txt = Instantiate<TMP_Text>(answer);
            txt.text = ans.text;
            txt.GetComponent<Button>().onClick.AddListener(delegate { showMessage(ans.msgID, ans.action); });
            txt.transform.SetParent(answers.transform);
        }
    }
    public void dialogueEnd()
    {
        dialogueWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam.GetComponent<Looking>().nrot = true;
        player.GetComponent<Move>().cam0.SetActive(false);
        FindObjectOfType<AudioManager>().Play("E");
    }
    public void gameEnd()
    {
        SceneManager.LoadScene(3);
    }
    public void kiss()
    {
        kisss.SetActive(true);
        player.GetComponent<Move>().jumpforse = 150f;
        FindObjectOfType<AudioManager>().Play("chmok");
    }
    public void setAction(string name, action act)
    {
        actions[name] = act;
    }
}
