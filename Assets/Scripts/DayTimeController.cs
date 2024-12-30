using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    #region Variables
    const float secondsInDay = 86400f;  //하루(24시간)를 초단위로 계산한 것 (60 x 60 x 24 = 86400)

    private float time;     //시간을 누적시킬 변수
    [SerializeField] private float timeScale = 60f; //시간의 흐름을 컨트롤 할 타임 스케일 변수

    [SerializeField] private Color nightLightColor; //밤에 사용될 컬러값
    [SerializeField] private Color dayLightColor;   //낮에 사용될 컬러값
    [SerializeField] private AnimationCurve nightTimeCurve; //DayLightColor -> NightLightColor 값으로 변경할때 적용시킬 AnimationCurve

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Light2D globalLight;

    [SerializeField] private int days = 0;
    private float Minutes
    {
        get { return time % 3600 / 60f; }
    }
    private float Hours
    {
        get { return time / 3600f; }
    }
    #endregion

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        int hours = (int)Hours;
        int minute = (int)Minutes;

        text.text = hours.ToString("00") + " : " + minute.ToString("00");

        float curveData = nightTimeCurve.Evaluate(Hours);
        Color changeColor = Color.Lerp(dayLightColor, nightLightColor, curveData);
        globalLight.color = changeColor;
        if (time > secondsInDay)
        {
            NextDay();
        }

    }

    private void NextDay()
    {
        time = 0f;
        days += 1;
    }
}
