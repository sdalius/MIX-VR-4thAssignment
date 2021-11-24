using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ChallengeButton : MonoBehaviour
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

    private TextMeshProUGUI machineSpeedText;
    private FireRateIncreaseSpeedButton increaseSpeedButton;
    private FireRateDeacreaseSpeedButton decreaseSpeedButton;

    private TurretShootProjectile machineObject;

    private SimpleShoot gunObject;

    private LeaderBoard leaderBoard;

    

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
        machineSpeedText = GameObject.FindGameObjectWithTag("FireRateText").GetComponent<TextMeshProUGUI>();
        increaseSpeedButton = FindObjectOfType<FireRateIncreaseSpeedButton>();
        decreaseSpeedButton = FindObjectOfType<FireRateDeacreaseSpeedButton>();
        machineObject = FindObjectOfType<TurretShootProjectile>();
        gunObject = FindObjectOfType<SimpleShoot>();
        leaderBoard = FindObjectOfType<LeaderBoard>();
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
                timeLeft -= Time.deltaTime;

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
        _isPressed = true;
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
        if (machineObject.getbIsShooting())
            machineObject.setbIsShooting(false);
        if (machineObject.getbProjectileToLaunch() == null)
        {
            challengeText.text = "Please put an item on the Item area!";
            yield break;
        }
        else
        machineObject.fireRate = 2f;
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
        startChallengeAttributes();
        InvokeRepeating("decreaseFireRate",10, 10);
    }

    private void decreaseFireRate(){
            Debug.Log("Decreasing fireRate");
            machineObject.decreaseFireRate();
    }

    private void startChallengeAttributes(){
        gunObject.resetNumberOfShotObjects();
        machineObject.clearNumOfShotPlates();
        bChallengeStarted = true;
        timer.enabled = true;
        machineSpeedText.enabled = false;
        increaseSpeedButton.enabled = false;
        decreaseSpeedButton.enabled = false;
        machineObject.setbIsShooting(true);
    }
    private void stopChallenge(){
        leaderBoard.insertPlayersScore(leaderBoard.generateName(false), gunObject.NumberofShotObjects());
        gunObject.resetNumberOfShotObjects();
        machineObject.clearNumOfShotPlates();
        bChallengeStarted=false;
        challengeText.enabled = false;
        timer.enabled =false;
        machineSpeedText.enabled = true;
        increaseSpeedButton.enabled = true;
        decreaseSpeedButton.enabled = true;
        machineObject.fireRate = 1f;
        timeLeft = challengeTime;
        machineObject.setbIsShooting(false);
        CancelInvoke("decreaseFireRate");
        leaderBoard.generateName(true);
    }
}
