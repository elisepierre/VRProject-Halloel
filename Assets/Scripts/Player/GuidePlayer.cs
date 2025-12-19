using UnityEngine;

public class GuidePlayer : MonoBehaviour
{
    [Header("Player & Minimap")]
    public Transform player;
    public RectTransform arrow;
    public Camera miniMapCamera;
    public float radius = 50f;

    [Header("Targets")]
    public Transform[] targets;
    public float targetRadius = 1f;
    private int currentTargetIndex = 0;

    [Header("Tutoriel")]
    public GhostTutorial ghostTutorial;

    [Header("Enemy")]
    public EnemySpawner enemySpawner;

    [Header("Player Abilities")]
    public PlayerAbilities playerAbilities;

    [Header("Boss")]
    public Boss boss;

    [Header("Santa Claus")]
    public Animator santaAnimator;


    void Start()
    {
        if (arrow != null)
            arrow.gameObject.SetActive(false);

        foreach (Transform t in targets)
            if (t != null)
                t.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null || arrow == null || miniMapCamera == null || targets.Length == 0)
            return;

        if (ghostTutorial != null && (ghostTutorial.tutorialActive || ghostTutorial.temporaryMessageActive))
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        else
        {
            arrow.gameObject.SetActive(true);
        }

        Transform currentTarget = targets[currentTargetIndex];
        if (!currentTarget.gameObject.activeSelf)
            currentTarget.gameObject.SetActive(true);

        Vector3 dir = currentTarget.position - player.position;
        Vector3 flatDir = new Vector3(dir.x, 0f, dir.z).normalized;

        if (dir.magnitude <= targetRadius)
        {
            OnTargetReached();
            return;
        }

        Vector3 viewportPos = miniMapCamera.WorldToViewportPoint(currentTarget.position);
        bool targetVisible = viewportPos.x >= 0f && viewportPos.x <= 1f &&
                             viewportPos.y >= 0f && viewportPos.y <= 1f &&
                             viewportPos.z > 0f;

        if (targetVisible)
        {
            arrow.localPosition = Vector3.zero;
            float angleToTarget = Mathf.Atan2(flatDir.x, flatDir.z) * Mathf.Rad2Deg - player.eulerAngles.y;
            arrow.localRotation = Quaternion.Euler(0f, 0f, -angleToTarget);
        }
        else
        {
            float angle = Mathf.Atan2(flatDir.x, flatDir.z) * Mathf.Rad2Deg - player.eulerAngles.y;
            arrow.localPosition = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius, Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0f);
            arrow.localRotation = Quaternion.Euler(0f, 0f, -angle);
        }
    }

    private void OnTargetReached()
    {
        Transform reachedTarget = targets[currentTargetIndex];
        reachedTarget.gameObject.SetActive(false);

        if (currentTargetIndex == 0)
        {
            if (enemySpawner != null)
                enemySpawner.StartSpawning();

            if (ghostTutorial != null)
                ghostTutorial.ShowTemporaryMessage("Attention ! Les citrouilles sont là !", ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
        }

        if (currentTargetIndex == 1)
        {
            if (enemySpawner != null)
                enemySpawner.SetSpawnInterval(2f);

            if (ghostTutorial != null)
                ghostTutorial.ShowTemporaryMessage("Les ennemis sont plus nombreux dans le cimetière, reste sur tes gardes !", ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
        }

        if (currentTargetIndex == 2)
        {
            if (playerAbilities != null)
                playerAbilities.UnlockAbility();

            if (ghostTutorial != null)
            {
                ghostTutorial.ShowTemporaryMessage(
                    "Tu as gagné la capacité d'étourdir les ennemis près de toi pendant 3 secondes !", 
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
                ghostTutorial.ShowTemporaryMessage(
                    "Juste ici la recharge de ta capacité disponible toutes les 10 secondes", 
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[11] : null);
                ghostTutorial.ShowTemporaryMessage(
                    "Pour l'activer, clic droit sur la souris !",
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
            }
        }

        if (currentTargetIndex == 3)
        {
            if (ghostTutorial != null)
                ghostTutorial.ShowTemporaryMessage(
                    "Super ! Continue ta route !", 
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
        }

        if (currentTargetIndex == 4)
        {
            if (ghostTutorial != null)
            {
                ghostTutorial.ShowTemporaryMessage(
                    "Tu m'as retrouvé ! Les citrouilles ont enfin disparus grâce à toi !",
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null
                );

                ghostTutorial.ShowTemporaryMessage(
                    "Mais un boss a volé les vêtements de mon ami barbu.",
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null
                );

                ghostTutorial.ShowTemporaryMessage(
                    "Regarde ta mini-map, une autre Target rose semble t'attendre...",
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[6] : null
                );
            }

            if (enemySpawner != null)
                enemySpawner.StopSpawning();

            Enemy.DestroyAllEnemies();

            if (santaAnimator != null)
            {
                santaAnimator.SetTrigger("PlayAnimation");
            }
        }


        if (currentTargetIndex == 5)
        {
            if (boss != null)
                boss.ActivateBoss();

            if (ghostTutorial != null)
                ghostTutorial.ShowTemporaryMessage(
                    "Voilà le boss !",
                    ghostTutorial.tutorialImages.Length > 0 ? ghostTutorial.tutorialImages[0] : null);
        }




        currentTargetIndex++;
        if (currentTargetIndex >= targets.Length)
            arrow.gameObject.SetActive(false);
    }

    public void ResetTargets()
    {
        currentTargetIndex = 0;
        foreach (Transform t in targets)
            if (t != null)
                t.gameObject.SetActive(false);
    }
}

