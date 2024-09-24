using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnswer
{
    public long answID = -1;
    public string text = "";
    public long msgID = -1;
    public string action = "";
}
public class CMessage
{
    public long msgID = -1;
    public string text = "";
    public List<CAnswer> answers = new List<CAnswer>();
}
public class CDialogue
{
    List<CMessage> messages = new List<CMessage>();
    long UID = 0;
    CMessage selectedMessage = null;
    CAnswer selectedAnswer = null;

    long getUID()
    {
        UID++;
        return UID;
    }

    CMessage findMsg(long msgID)
    {
        CMessage msg = null;
        foreach (CMessage i in messages)
        {
            if (i.msgID == msgID)
                msg = i;
        }
        return msg;
    }

    CAnswer findAnsw(long answID)
    {
        CAnswer answ = null;
        foreach (CAnswer i in selectedMessage.answers)
        {
            if (i.answID == answID)
                answ = i;
        }
        return answ;
    }
    public string selectMessage(long msgID)
    {
        selectedMessage = findMsg(msgID);
        return selectedMessage.text;
    }
    public string selectAnswer(long msgID, long answID)
    {
        selectMessage(msgID);
        selectedAnswer = findAnsw(answID);

        return selectedAnswer.text + "[action: " + selectedAnswer.action + " ]";
    }
    public List<CMessage> getMessages()
    {
        return messages;
    }
    public long linkedUID()
    {
        return selectedAnswer.msgID;
    }
    public void Clear()
    {
        messages.Clear();
        UID = 0;
        selectedMessage = null;
        selectedAnswer = null;
    }
    public void loadMessage(CMessage msg)
    {
        messages.Add(msg);
        selectedMessage = msg;
    }
    public void loadAnswer(CAnswer answ)
    {
        selectedMessage.answers.Add(answ);
    }
    public List<CAnswer> getAnswers()
    {
        return selectedMessage.answers;
    }
}
