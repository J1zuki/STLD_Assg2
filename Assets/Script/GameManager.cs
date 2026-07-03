/*
 * Description: Manages collectibles, objective UI, death UI, restart, and game completion.
 */

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CollectibleType;

/// Controls the main game progress, collectible count, death state, and escape state.
public class GameManager : MonoBehaviour
{
    /// Allows other scripts to access the GameManager.
    public static GameManager instance;

    /// Number of scrap pieces needed.
    [SerializeField] private int requiredScrap = 3;

    /// Number of energy coils needed.
    [SerializeField] private int requiredEnergyCoil = 5;

    /// Current scrap collected.
    private int currentScrap;

    /// Current energy coils collected.
    private int currentEnergyCoil;

    /// Checks if fuel has been collected.
    private bool hasFuel;

    /// Text that shows the current objective.
    [SerializeField] private TMP_Text objectiveText;

    /// Text that shows interaction prompt.
    [SerializeField] private TMP_Text promptText;

    /// Text that shows short player messages.
    [SerializeField] private TMP_Text messageText;

    /// Panel shown when player dies.
    [SerializeField] private GameObject restartPanel;

    /// Panel shown when player reaches spacecraft with all items.
    [SerializeField] private GameObject takeOffPanel;

    /// Panel shown when player escapes.
    [SerializeField] private GameObject gameEndPanel;

    /// Stores whether the player is dead.
    private bool isDead;

    /// Sets the singleton instance.
    private void Awake()
    {
        instance = this;
    }

    /// Sets the starting UI state.
    private void Start()
    {
        Time.timeScale = 1f;

        restartPanel.SetActive(false);
        takeOffPanel.SetActive(false);
        gameEndPanel.SetActive(false);

        ClearPrompt();
        ShowMessage("");
        UpdateObjectiveUI();
    }

    /// Checks for restart input after death.
    private void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    /// Adds a collectible to the player's progress.
    /// <param name="collectibleType">The collectible type picked up.</param>
    public void CollectItem(CollectibleType collectibleType)
    {
        if (collectibleType == CollectibleType.Scrap)
        {
            currentScrap++;
            ShowMessage("Scrap collected.");
        }
        else if (collectibleType == CollectibleType.EnergyCoil)
        {
            currentEnergyCoil++;
            ShowMessage("Energy Coil collected.");
        }
        else if (collectibleType == CollectibleType.Fuel)
        {
            hasFuel = true;
            ShowMessage("Fuel collected.");
        }

        UpdateObjectiveUI();
    }

    /// Checks if all required items have been collected.
    /// <returns>True if player has all required collectibles.</returns>
    public bool HasAllCollectibles()
    {
        return currentScrap >= requiredScrap &&
               currentEnergyCoil >= requiredEnergyCoil &&
               hasFuel;
    }

    /// Shows the restart UI when player dies.
    /// <param name="deathMessage">The reason the player died.</param>
    public void PlayerDied(string deathMessage)
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        ShowMessage(deathMessage);
        restartPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// Restarts the current scene.
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// Shows take off ready panel if all collectibles are collected.
    public void ShowTakeOffReady()
    {
        if (HasAllCollectibles())
        {
            takeOffPanel.SetActive(true);
            ShowMessage("All parts collected. Press E to take off.");
        }
        else
        {
            ShowMessage("Spacecraft is not ready. Find all repair parts first.");
        }
    }

    /// Hides the take off ready panel.
    public void HideTakeOffReady()
    {
        takeOffPanel.SetActive(false);
    }

    /// Ends the game when player escapes.
    public void CompleteGame()
    {
        gameEndPanel.SetActive(true);
        ShowMessage("You repaired the spacecraft and escaped.");
        Time.timeScale = 0f;
    }

    /// Shows interaction prompt.
    /// <param name="text">Prompt message to show.</param>
    public void ShowPrompt(string text)
    {
        promptText.text = text;
    }

    /// Clears interaction prompt.
    public void ClearPrompt()
    {
        promptText.text = "";
    }

    /// Shows a short message.
    /// <param name="text">Message to show.</param>
    public void ShowMessage(string text)
    {
        messageText.text = text;
    }

    /// Updates objective text.
    private void UpdateObjectiveUI()
    {
        objectiveText.text =
            "Scrap: " + currentScrap + " / " + requiredScrap +
            "\nEnergy Coil: " + currentEnergyCoil + " / " + requiredEnergyCoil +
            "\nFuel: " + (hasFuel ? "Collected" : "Missing");
    }
}