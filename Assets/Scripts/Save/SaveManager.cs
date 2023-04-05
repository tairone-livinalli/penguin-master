using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
  public static SaveManager Instance { get { return instance; } }
  private static SaveManager instance;

  private SaveState save;
  public string HighScoreToText() => save.HighScore.ToString("0000000");
  public string FishAmountToText() => save.Fish.ToString("000");
  private const string saveFileName = "save.pmss";
  private BinaryFormatter formatter;

  public Action<SaveState> OnLoadState;
  public Action<SaveState> OnSaveState;

  private void Awake()
  {
    instance = this;
    formatter = new BinaryFormatter();
    LoadPreviousSaveFile();
  }

  private void LoadPreviousSaveFile()
  {
    try
    {
      FileStream file = new FileStream(Application.persistentDataPath + "/" + saveFileName, FileMode.Open, FileAccess.Read);
      save = formatter.Deserialize(file) as SaveState;
      file.Close();
      OnLoadState?.Invoke(save);
    }
    catch (System.Exception)
    {
      Save();
    }
  }

  public void Save()
  {
    if (save == null)
    {
      save = new SaveState();
    }

    if (save.HighScore < (int)GameStats.Instance.CurrentScore())
    {
      save.HighScore = (int)GameStats.Instance.CurrentScore();
    }

    save.Fish += GameStats.Instance.CurrentFishAmount();

    save.LastSaveTime = DateTime.Now;
    FileStream file = new FileStream(Application.persistentDataPath + "/" + saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
    formatter.Serialize(file, save);
    file.Close();

    OnSaveState?.Invoke(save);
  }
}
