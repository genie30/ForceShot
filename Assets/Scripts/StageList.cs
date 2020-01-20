using System.Collections.Generic;
using UnityEngine;

// ステージデータ管理
public class StageList : MonoBehaviour
{
    public List<float[]> playerList = new List<float[]>();
    public List<float[]> holeList = new List<float[]>();
    public List<float[]> wallList = new List<float[]>();
    public List<int[]> etcList = new List<int[]>();

　// ティー位置の座標
    private void PlayerList()
    {
        var stage1 = new float[3] { -500.4995f, 2.06f, -384.0851f };
        var stage2 = new float[3] { -496.4f, 2.05f, -372.9f };
        var stage3 = new float[3] { -563.9f, 2.151f, -361.74f };
        var stage4 = new float[3] { -246.32f, 18.89f, -175.51f };
        var stage5 = new float[3] { -205.271f, 3.11826f, -172.67f };
        var stage6 = new float[3] { -100.19f, 3.11826f, -402.02f };
        var stage7 = new float[3] { -437.65f, 9.35f, -436.29f };
        var stage8 = new float[3] { -69.91f, 4.037112f, -115.76f };
        var stage9 = new float[3] { -611.1f, 2.14826f, -455.6f };
        var stage10 = new float[3] { -634f, 5.94f, -222.81f };
        var stage11 = new float[3] { -329f, 123.69f, -422f };
        var stage12 = new float[3] { -453.719f, 6.976503f, -379.39f };
        var stage13 = new float[3] { -313.37f, 10.03f, -274.68f };
        var stage14 = new float[3] { -185.54f, 101.3f, -58.23f };
        var stage15 = new float[3] { -235.22f, 7.89f, -486f };
        var stage16 = new float[3] { -205.917f, 7.89f, -410.548f };
        var stage17 = new float[3] { -56.2f, 4.08826f, -72.5f };
        var stage18 = new float[3] { -471.65f, 2.388528f, -186.0845f };
        var stage19 = new float[3] { -237.53f, 11.8f, -307.28f };
        var stage20 = new float[3] { -185.75f, 101.3f, -43.961f };

        playerList.Add(stage1);
        playerList.Add(stage2);
        playerList.Add(stage3);
        playerList.Add(stage4);
        playerList.Add(stage5);
        playerList.Add(stage6);
        playerList.Add(stage7);
        playerList.Add(stage8);
        playerList.Add(stage9);
        playerList.Add(stage10);
        playerList.Add(stage11);
        playerList.Add(stage12);
        playerList.Add(stage13);
        playerList.Add(stage14);
        playerList.Add(stage15);
        playerList.Add(stage16);
        playerList.Add(stage17);
        playerList.Add(stage18);
        playerList.Add(stage19);
        playerList.Add(stage20);
    }

    // ホール座標
    private void HoleList()
    {
        //位置x,y,z 回転y
        var stage1 = new float[4] { -522.42f, 9.255f, -353.42f, 0f };
        var stage2 = new float[4] { -321.75f, 3.0279f, -316.52f, -90f };
        var stage3 = new float[4] { -615.8098f, 2.0566f, -248.57f, 90f };
        var stage4 = new float[4] { -529.35f, 2.1587f, -153.64f, 1.5f };
        var stage5 = new float[4] { -184.66f, 5.3591f, -406.22f, -100f };
        var stage6 = new float[4] { -39.4f, 9.4495f, -88.4f, -150f };
        var stage7 = new float[4] { -440f, 29.209f, -420.37f, 100f };
        var stage8 = new float[4] { -466.42f, 13.436f, -104.83f, 95f };
        var stage9 = new float[4] { -514.38f, 12.675f, -531.79f, -70f };
        var stage10 = new float[4] { -35.5f, 6.819f, -282.77f, -20f };
        var stage11 = new float[4] { -387.43f, 5f, -353.3f, 110f };
        var stage12 = new float[4] { -391.63f, 3.042f, -124f, -0.6f };
        var stage13 = new float[4] { -552.73f, 6.333f, -266.54f, -113.3f };
        var stage14 = new float[4] { -436.89f, 49.147f, -152.45f, 0f };
        var stage15 = new float[4] { -668f, 49.64f, -16.56f, -37.5f };
        var stage16 = new float[4] { -205.917f, 2.93f, -410.548f, 152.3f };
        var stage17 = new float[4] { -253.52f, 2.929f, -326.6f, 57.52f };
        var stage18 = new float[4] { -327.71f, 123.6f, -422.35f, -165.95f };
        var stage19 = new float[4] { -287.85f, 11.7f, -281.21f, 55.4f };
        var stage20 = new float[4] { -340.6f, 123.6f, -487.1f, 22.16f };

        holeList.Add(stage1);
        holeList.Add(stage2);
        holeList.Add(stage3);
        holeList.Add(stage4);
        holeList.Add(stage5);
        holeList.Add(stage6);
        holeList.Add(stage7);
        holeList.Add(stage8);
        holeList.Add(stage9);
        holeList.Add(stage10);
        holeList.Add(stage11);
        holeList.Add(stage12);
        holeList.Add(stage13);
        holeList.Add(stage14);
        holeList.Add(stage15);
        holeList.Add(stage16);
        holeList.Add(stage17);
        holeList.Add(stage18);
        holeList.Add(stage19);
        holeList.Add(stage20);
    }

    // コースの壁座標、サイズ
    private void WallList()
    {
        //Wall 位置x,y,z サイズx,y,z
        var stage1 = new float[6] { -509.4f, 10.8f, -356.5f, 36.8f, 25f, 73.1f };
        var stage2 = new float[6] { -395f, 40f, -335f, 235f, 90f, 134.5f };
        var stage3 = new float[6] { -595f, 6f, -313.1f, 130f, 25f, 134.5f };
        var stage4 = new float[6] { -383f, 40f, -173f, 350f, 90f, 75f };
        var stage5 = new float[6] { -198.6f, 8f, -289.4f, 39.8f, 20f, 263f };
        var stage6 = new float[6] { -78.4f, 40f, -256.7f, 110f, 90f, 408.8f };
        var stage7 = new float[6] { -439.81f, 31.3f, -437.14f, 10f, 50f, 38f };
        var stage8 = new float[6] { -282.39f, 40f, -100f, 482.5f, 90f, 110f };
        var stage9 = new float[6] { -564.7f, 22f, -490.6f, 140f, 55f, 110f };
        var stage10 = new float[6] { -340.7f, 8f, -250.4f, 620.8f, 20f, 90f };
        var stage11 = new float[6] { -407.1f, 95f, -351.7f, 200f, 200f, 200f };
        var stage12 = new float[6] { -358f, 95f, -236.2f, 280f, 200f, 368.5f };
        var stage13 = new float[6] { -435.8f, 17f, -322.1f, 280f, 42f, 114.9f };
        var stage14 = new float[6] { -336.7f, 95f, -117.5f, 400f, 200f, 211f };
        var stage15 = new float[6] { -340.1f, 45f, -263f, 663f, 100f, 500f };
        var stage16 = new float[6] { -195.72f, 8f, -407.87f, 25.1f, 20f, 9.8f };
        var stage17 = new float[6] { -160.41f, 8f, -222.1f, 294f, 20f, 350f };
        var stage18 = new float[6] { -346f, 135f, -294f, 540f, 300f, 480f };
        var stage19 = new float[6] { -266.2f, 16.2f, -313f, 68f, 11.7f, 77.9f };
        var stage20 = new float[6] { -297f, 100f, -258f, 280f, 150f, 500f };

        wallList.Add(stage1);
        wallList.Add(stage2);
        wallList.Add(stage3);
        wallList.Add(stage4);
        wallList.Add(stage5);
        wallList.Add(stage6);
        wallList.Add(stage7);
        wallList.Add(stage8);
        wallList.Add(stage9);
        wallList.Add(stage10);
        wallList.Add(stage11);
        wallList.Add(stage12);
        wallList.Add(stage13);
        wallList.Add(stage14);
        wallList.Add(stage15);
        wallList.Add(stage16);
        wallList.Add(stage17);
        wallList.Add(stage18);
        wallList.Add(stage19);
        wallList.Add(stage20);
    }

    // 星条件、クリア時のExp
    private void EtcList()
    {
        //星123の条件 Exp
        var stage1 = new int[4] { 10, 4, 2, 80 };
        var stage2 = new int[4] { 10, 5, 3, 100 };
        var stage3 = new int[4] { 10, 5, 3, 120 };
        var stage4 = new int[4] { 10, 6, 4, 140 };
        var stage5 = new int[4] { 10, 6, 4, 160 };
        var stage6 = new int[4] { 12, 7, 5, 180 };
        var stage7 = new int[4] { 10, 5, 4, 200 };
        var stage8 = new int[4] { 16, 8, 4, 240 };
        var stage9 = new int[4] { 15, 8, 5, 280 };
        var stage10 = new int[4] { 15, 10, 5, 300 };
        var stage11 = new int[4] { 7, 5, 3, 330 };
        var stage12 = new int[4] { 10, 6, 4, 360 };
        var stage13 = new int[4] { 8, 5, 3, 400 };
        var stage14 = new int[4] { 10, 8, 5, 450 };
        var stage15 = new int[4] { 12, 9, 6, 500 };
        var stage16 = new int[4] { 3, 2, 1, 520 };
        var stage17 = new int[4] { 12, 8, 4, 540 };
        var stage18 = new int[4] { 10, 6, 3, 560 };
        var stage19 = new int[4] { 3, 2, 1, 580 };
        var stage20 = new int[4] { 10, 7, 5, 600 };

        etcList.Add(stage1);
        etcList.Add(stage2);
        etcList.Add(stage3);
        etcList.Add(stage4);
        etcList.Add(stage5);
        etcList.Add(stage6);
        etcList.Add(stage7);
        etcList.Add(stage8);
        etcList.Add(stage9);
        etcList.Add(stage10);
        etcList.Add(stage11);
        etcList.Add(stage12);
        etcList.Add(stage13);
        etcList.Add(stage14);
        etcList.Add(stage15);
        etcList.Add(stage16);
        etcList.Add(stage17);
        etcList.Add(stage18);
        etcList.Add(stage19);
        etcList.Add(stage20);
    }

    public static StageList instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerList();
        HoleList();
        WallList();
        EtcList();
    }
}
