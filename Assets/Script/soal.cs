using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class soal : MonoBehaviour
{
    public GameObject originalGameObject;
    public Text var1;  
    public Text operand;  
    public Text var2;
    public Image imageSoal1;
    public Image imageSoal2; 
    public Image imageSoal3;
    public Button jawab1;
    public Button jawab2;
    public Button jawab3;
    public GameObject papanSkor; 
    public GameObject feedBenar,feedSalah,selesai;  
    public AudioSource m_Benar,m_Salah,m_SkorTinggiBaru;

    //first value
    private int lowFirstValue = 1;
    private int highFirstValue = 10;
    public int firstValue;
    //second value
    private int lowSecondValue = 1;
    private int highSecondValue = 10;
    public int SecondValue;
    public int finalValue;
    private bool isAdd = false;
    private bool isSub = false;
    private int skor = 0;
    private int skorTinggi=0;
    private bool isNewHScore = false;
    

    public Sprite[] m_Sprite;
    public Sprite[] n_Sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("skorTinggi"))
        {
            skorTinggi = PlayerPrefs.GetInt("skorTinggi");
        }
        generateSoal();
        isiTextSoal();
        isiImageSoal();
        isiJawabButton();
    }

    public void generateSoal()
    {
        var i = Random.Range(0, 2);

        if (i == 0)
        {
            firstValue = Random.Range(lowFirstValue, highFirstValue);
            highSecondValue = 10 - firstValue;
            SecondValue = Random.Range(lowSecondValue, highSecondValue);
            finalValue = firstValue + SecondValue;
            isAdd = true;
        }
        if (i == 1)
        {
            firstValue = Random.Range(lowFirstValue+1, highFirstValue);
            highSecondValue = firstValue-1;
            SecondValue = Random.Range(lowSecondValue, highSecondValue);
            finalValue = firstValue - SecondValue;
            isSub = true;
        }
    }

    public void isiTextSoal()
    {
        var1.text = firstValue.ToString();
        var2.text = SecondValue.ToString();
        if(isAdd)
        {
            operand.text = '+'.ToString();
        }
        if(isSub)
        {
            operand.text = '-'.ToString();
        }

    }

    public void isiImageSoal()
    {
        imageSoal1.sprite = m_Sprite[firstValue-1];
        if(isSub)
        {
            imageSoal2.sprite = n_Sprite[SecondValue-1];
        }
        else{
             imageSoal2.sprite = m_Sprite[SecondValue-1];
        }
        imageSoal3.sprite = m_Sprite[finalValue-1];
    }

    public void isiJawabButton()
    {
        var i = Random.Range(0, 3);
        int j,l;
        if(i==0)
        {
            jawab1.GetComponentInChildren<Text>().text = finalValue.ToString();
            do
            {
                 j = Random.Range(1,11);
                 l = Random.Range(1,11);
            }while(j == finalValue || l == j || l == finalValue);
            jawab2.GetComponentInChildren<Text>().text = j.ToString();
            jawab3.GetComponentInChildren<Text>().text = l.ToString();
        }
        else if (i==1)
        {
            jawab2.GetComponentInChildren<Text>().text = finalValue.ToString();
            do
            {
                 j = Random.Range(1,11);
                 l = Random.Range(1,11);
            }while(j == finalValue || l == j || l == finalValue);
            jawab1.GetComponentInChildren<Text>().text = j.ToString();
            jawab3.GetComponentInChildren<Text>().text = l.ToString();
        }
        else
        {
            jawab3.GetComponentInChildren<Text>().text = finalValue.ToString();
            do
            {
                 j = Random.Range(1,11);
                 l = Random.Range(1,11);
            }while(j == finalValue || l == j || l == finalValue);
            jawab1.GetComponentInChildren<Text>().text = j.ToString();
            jawab2.GetComponentInChildren<Text>().text = l.ToString();
        }
        
    }

    public void jawab(){
        int jawaban = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
        Debug.Log(jawaban);
        if (jawaban == finalValue) 
        {   
            m_Benar.Play();
            feedBenar.SetActive(false);
            feedBenar.SetActive(true);
            skor+=5;
            if(skor>skorTinggi)
            {
                isNewHScore = true;
                skorTinggi=skor;
                PlayerPrefs.SetInt("skorTinggi",skorTinggi);
            }
            papanSkor.GetComponentInChildren<Text>().text = skor.ToString();
            isAdd = false;
            isSub = false;
            generateSoal();
            isiTextSoal();
            isiImageSoal();
            isiJawabButton();
        }
        else{
            m_Salah.Play();
            Text[] textSelesai;
            feedSalah.SetActive(false);
            feedSalah.SetActive(true);
            if(isNewHScore)
            {
                m_SkorTinggiBaru.Play();
            }
            selesai.SetActive(true);
            textSelesai = selesai.GetComponentsInChildren<Text>();
            string skorAkhir = "Skor : " + skor;
            string skorTinggiAkhir = "Skor Tertinggi : " + skorTinggi;
            textSelesai[1].text=skorAkhir;
            textSelesai[2].text=skorTinggiAkhir;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
