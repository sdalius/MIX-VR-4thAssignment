using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PhysicsButton : MonoBehaviour
{
    private TextMeshProUGUI challengeText;
    private TextMeshProUGUI timer;
    [SerializeField] private float challengeTime = 60;
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = 0.025f;
    private bool bChallengeStarted = false;
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    public UnityEvent onPressed, onReleased;

    private float timeLeft{get;set;}
    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f,1f);
    }
     
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
        challengeText = GameObject.FindGameObjectWithTag("ChallengeTextTimer").GetComponent<TextMeshProUGUI>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
        timeLeft = challengeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        Pressed();

        if (_isPressed && GetValue() - threshold <= 0)
        Released();

        if (bChallengeStarted)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime * 4;

                string minutesLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
                string seconds = (timeLeft % 60).ToString("F0");
                seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;
                timer.text = minutesLeft + ":" + seconds;
            }
            else{
                stopChallenge();
            }
        }
    }

    private void Pressed()
    {
        onPressed.Invoke();
        Debug.Log("Pressed");
        if(bChallengeStarted)
            return;
        challengeText.enabled = true;
        StartCoroutine(challengeBegin());
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }

    private IEnumerator challengeBegin()
    {
        challengeText.text = "Prepare for 1 min challenge!";
        yield return new WaitForSeconds(2);
        challengeText.text = "Challenge will start in 5";
        yield return new WaitForSeconds(1);
        challengeText.text = "Challenge will start in 4";
        yield return new WaitForSeconds(1);
        challengeText.text = "Challenge will start in 3";
        yield return new WaitForSeconds(1);
        challengeText.text = "Challenge will start in 2";
        yield return new WaitForSeconds(1);
        challengeText.text = "Challenge will start in 1";
        yield return new WaitForSeconds(1);
        challengeText.text = "GO!";
        timer.enabled = true;
        bChallengeStarted = true;
        // Make a function to start the machine
    }
    private void stopChallenge(){
        bChallengeStarted=false;
        challengeText.enabled = false;
        timer.enabled =false;
        timeLeft = challengeTime;
    }
}