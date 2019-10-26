using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadManager : MonoBehaviour
{
    public Text EnergyCount;
    public Text StarsCount;
    public Text MoneyCount;
    public Text lvl;
    public Text Rang;
    public GameObject canvas;

    //для панели настроек
    public Button settings;
    public GameObject sett_panel;
    public Button screensize;
    public Button mute;
    public Button volume;
    public Button close;
    //регулятор громкости
    public GameObject vol;
    private Scrollbar vol_scroll;
    

    //постоянно управляет обновлением данных в верхней панельки сцены
    //количеством энергии
    //количеством денег
    //количеством звёзд
    //строкой ранга
    //цифрой уровня
    void Start()
    {
        //задать расширение при старте

        settings.onClick.AddListener(SettingsClick);
        screensize.onClick.AddListener(ScreenSize);
        mute.onClick.AddListener(Mute);
        close.onClick.AddListener(Close);
        volume.onClick.AddListener(Volume);

        //взять компонент скролл
        //и указать какую функцию вызывать при изменении значения
        vol_scroll = vol.GetComponent<Scrollbar>();
        vol_scroll.onValueChanged.AddListener(delegate { Player.SetVol(vol_scroll.value); });


        lvl.text = (Player.lvl + 1).ToString();
        EnergyCount.text = Player.energy.ToString()+"/"+DataLoad.levels[Player.lvl].maxE;
        StarsCount.text = Player.stars.ToString() + "/" + DataLoad.levels[Player.lvl].maxS;
        MoneyCount.text = Player.money.ToString();
        Rang.text = DataLoad.levels[Player.lvl].name;


    }

    // Update is called once per frame
    void Update()
    {
        lvl.text = (Player.lvl+1).ToString();
        EnergyCount.text = Player.energy.ToString() + "/" + DataLoad.levels[Player.lvl].maxE;
        if( Player.lvl==8)
            StarsCount.text = Player.stars.ToString();
        else
            StarsCount.text = Player.stars.ToString() + "/" + DataLoad.levels[Player.lvl].maxS;
        MoneyCount.text = Player.money.ToString();
        Rang.text = DataLoad.levels[Player.lvl].name;
    }

    //включение-выключение панели меню
    private void SettingsClick()
    {
        sett_panel.SetActive(!sett_panel.activeSelf);
        //выключая панель выключи и слайдер
        if (vol.activeSelf)
            Volume();
    }

    //изменение разрешения приложения
    //фуллскрин или нет
    private void ScreenSize()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    //для включения-выключения звука
    private void Mute()
    {
        Player.MuteMusic();
    }
    //для изменения настроек громкости
    private void Volume()
    {
        if (vol.activeSelf)
            vol.transform.SetParent(transform);
        if (!vol.activeSelf)
            vol.transform.SetParent(canvas.transform);
        vol.SetActive(!vol.activeSelf);
    }
    //для вызова подсказки
    private void Close()
    {
        Application.Quit();
    }

}
