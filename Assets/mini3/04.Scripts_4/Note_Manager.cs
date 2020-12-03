using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Note_Manager : MonoBehaviour {

    public static Note_Manager instance;

    public Transform[] note = new Transform[18];
    public Transform[] note_cover = new Transform[9];
    public Transform Checker;
    public Sprite[] instrument = new Sprite[4];
    string[] lines = new string[70];
    string writer;
    string Song_name;

    int note_set_count = 0;
    int read_line = 2;

    float[] note_x_point = new float[9];
    float[] note_y_point = new float[] { -0.615f, -2.2463f };

    int[,] top_note = new int[9, 2];
    bool[,] success_history = new bool[9, 2];
    int top_note_amount = 0;
    int[,] bottom_note = new int[9, 2];
    int bottom_note_amount = 0;

    public void read_data()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            TextAsset txtAsset = (TextAsset)Resources.Load("note_data", typeof(TextAsset));
            lines = txtAsset.text.Split('\n');
            string[] data = lines[0].Split(',');
            Song_name = data[0];
            writer = data[1];
        }
        else
        {
            TextAsset txtAsset = (TextAsset)Resources.Load("note_data", typeof(TextAsset));
            lines = txtAsset.text.Split('\n');
            string[] data = lines[0].Split(',');
            Song_name = data[0];
            writer = data[1];
        }
    }

    public bool get_node_state()
    {
        if (note_set_count < int.Parse(lines[1]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool get_read_state()
    {
        if (top_note[0, 0] != -1 || bottom_note[0, 0] != -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void init_top_node()
    {
        for (int i = 0; i < 9; i++)
        {
            top_note[i, 0] = -1;
            note[i].gameObject.SetActive(false);
            success_history[i, 0] = false;
        }
        top_note_amount = 0;
    }

    public void init_bottom_node()
    {
        for (int i = 0; i < 9; i++)
        {
            bottom_note[i, 0] = -1;
            note[i + 9].gameObject.SetActive(false);
            success_history[i, 1] = false;
        }
        bottom_note_amount = 0;
    }

    public void init_note_cover()
    {
        for (int i = 0; i < 9; i++)
        {
            note_cover[i].gameObject.SetActive(false);
        }
    }

    public void set_top_node()
    {
        if (note_set_count < int.Parse(lines[1]))
        {
            top_note_amount = int.Parse(lines[read_line]);
            int count = 0;
            read_line++;

            string[] data = lines[read_line].Split(',');
            for (int i = 0 ; i < top_note_amount; i++ )
            {
                if (data[i*2].Equals("Ta"))
                {
                    //Debug.Log("탬버린");
                    top_note[count, 0] = 0;
                }
                else if (data[i*2].Equals("Ma"))
                {
                    //Debug.Log("마라카스");
                    top_note[count, 0] = 1;
                }
                else if (data[i*2].Equals("Ca"))
                {
                    //Debug.Log("캐스터네츠");
                    top_note[count, 0] = 2;
                }
                else if (data[i*2].Equals("Cl"))
                {
                    //Debug.Log("박수");
                    top_note[count, 0] = 3;
                }
                top_note[count, 1] = int.Parse(data[i*2+1]);
                count++;
            }
            read_line++;
            note_set_count = note_set_count + 1;

            for (int i = 0; i < top_note_amount; i++)
            {
                note[i].GetComponent<SpriteRenderer>().sprite = instrument[top_note[i, 0]];
                note[i].position = new Vector3((float)(note_x_point[top_note[i, 1] - 1]), (float)note_y_point[0], 90f);
                note[i].gameObject.SetActive(true);
            }
        }
        else
        {
            init_top_node();
        }
    }
      
    public void set_bottom_node()
    {
        if (note_set_count < int.Parse(lines[1]))
        {
            bottom_note_amount = int.Parse(lines[read_line]);
            int count = 0;
            read_line++;

            string[] data = lines[read_line].Split(',');
            for (int i = 0; i < bottom_note_amount; i++)
            {
                if (data[i * 2].Equals("Ta"))
                {
                    //Debug.Log("탬버린");
                    bottom_note[count, 0] = 0;
                }
                else if (data[i * 2].Equals("Ma"))
                {
                    //Debug.Log("마라카스");
                    bottom_note[count, 0] = 1;
                }
                else if (data[i * 2].Equals("Ca"))
                {
                    //Debug.Log("캐스터네츠");
                    bottom_note[count, 0] = 2;
                }
                else if (data[i * 2].Equals("Cl"))
                {
                    //Debug.Log("박수");
                    bottom_note[count, 0] = 3;
                }
                bottom_note[count, 1] = int.Parse(data[i * 2 + 1]);
                count++;
            }
            read_line++;
            note_set_count = note_set_count + 1;

            for (int i = 0; i < bottom_note_amount; i++)
            {
                note[i + 9].GetComponent<SpriteRenderer>().sprite = instrument[bottom_note[i, 0]];
                note[i + 9].position = new Vector3((float)(note_x_point[bottom_note[i, 1] - 1]), (float)note_y_point[1], 90f);
                note[i + 9].gameObject.SetActive(true);
            }
        }
        else
        {
            init_bottom_node();
        }
    }

    void set_note_cover(int x_point, int y_point, int number)
    {
        note_cover[number].position = new Vector3(note_x_point[x_point], note_y_point[y_point], number);
        note_cover[number].gameObject.SetActive(true);
    }

    public void Click_point_check(int select_button)
    {
        Vector3 check_point = Checker.position;
        bool result = false;
        float point_center = check_point.x;
        float point_left = check_point.x - 0.2166f;
        float point_right = check_point.x + 0.2166f;

        if(Game_Manager_4.instance.get_p_g_state() == 1)
        {
            for(int i = 0; i < top_note_amount; i++)
            {
                if(result == true)
                {
                    break;
                }
                else
                {
                    float note_center = note_x_point[top_note[i, 1]-1];
                    float note_left = note_center - 0.2166f;
                    float note_right = note_center + 0.2166f;
                    if(note_center == point_center || (point_right > note_left && Mathf.Abs(point_right-note_left) < 0.4332) || (point_left < note_right && Mathf.Abs(note_right - point_left) < 0.4332))
                    {
                        if(select_button == top_note[i ,0] && success_history[i,0] == false)
                        {
                            result = true;
                            success_history[i, 0] = true;
                            set_note_cover(top_note[i, 1] - 1, 0, i);
                            //Debug.Log(i);
                            if(Mathf.Abs(note_center - point_center) <= 0.2166 / 4)
                            {
                                Game_Manager_4.instance.add_point(3);
                                Effect_Manager_4.instance.floating_text_effect(3);
                            }
                            else if(Mathf.Abs(note_center - point_center) <= (0.2166*3) / 4)
                            {
                                Game_Manager_4.instance.add_point(2);
                                Effect_Manager_4.instance.floating_text_effect(2);
                            }
                            else
                            {
                                Game_Manager_4.instance.add_point(1);
                                Effect_Manager_4.instance.floating_text_effect(1);
                            }
                        }
                    }
                }
            }
            
            if(result == false)
            {
                Game_Manager_4.instance.minus_point();
                Effect_Manager_4.instance.floating_text_effect(0);
            }
        }
        else if(Game_Manager_4.instance.get_p_g_state() == 2)
        {
            for (int i = 0; i < bottom_note_amount; i++)
            {
                if (result == true)
                {
                    break;
                }
                else
                {
                    float note_center = note_x_point[bottom_note[i, 1] - 1];
                    float note_left = note_center - 0.2166f;
                    float note_right = note_center + 0.2166f;
                    if (note_center == point_center || (point_right > note_left && Mathf.Abs(point_right - note_left) < 0.4332) || (point_left < note_right && Mathf.Abs(note_right - point_left) < 0.4332))
                    {
                        if (select_button == bottom_note[i, 0] && success_history[i, 1] == false)
                        {
                            result = true;
                            success_history[i, 1] = true;
                            set_note_cover(bottom_note[i, 1] - 1, 1, i);
                            //Debug.Log(i);
                            if (Mathf.Abs(note_center - point_center) <= 0.2166 / 4)
                            {
                                Game_Manager_4.instance.add_point(3);
                                Effect_Manager_4.instance.floating_text_effect(3);
                            }
                            else if (Mathf.Abs(note_center - point_center) <= (0.2166 * 3) / 4)
                            {
                                Game_Manager_4.instance.add_point(2);
                                Effect_Manager_4.instance.floating_text_effect(2);
                            }
                            else
                            {
                                Game_Manager_4.instance.add_point(1);
                                Effect_Manager_4.instance.floating_text_effect(1);
                            }
                        }
                    }
                }
            }

            if (result == false)
            {
                Game_Manager_4.instance.minus_point();
                Effect_Manager_4.instance.floating_text_effect(0);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < 9;i++)
        {
            note_x_point[i] = -2.368f + 0.4686f * (i+1);
        }
        read_data();
        Effect_Manager_4.instance.set_Song_Data(Song_name, writer);
        init_top_node();
        init_bottom_node();
        init_note_cover();
        set_top_node();
        set_bottom_node();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        if (Note_Manager.instance == null)
        {
            Note_Manager.instance = this;
        }
        //초기화
    }
}
