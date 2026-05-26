using System.Collections;
using UnityEngine;

public class SprinklerLeverController : MonoBehaviour
{
    [Header("물 파티클들")]
    public ParticleSystem[] waterParticles;

    [Header("설정")]
    public float sprayDuration = 3f;
    public string chemicalTag = "ChemicalPuddle";

    private bool isActivated = false;

    public void ActivateSprinklers()
    {
        if (isActivated) return;

        isActivated = true;

        StartCoroutine(SprayAndClean());
    }

    IEnumerator SprayAndClean()
    {
        // 물 파티클 실행
        foreach (ParticleSystem ps in waterParticles)
        {
            if (ps != null)
            {
                Debug.Log("물 실행: " + ps.name);

                ps.gameObject.SetActive(true);

                ps.Play();
            }
            else
            {
                Debug.Log("비어있는 파티클 있음");
            }
        }

        // 의약품 찾기
        GameObject[] chemicals =
            GameObject.FindGameObjectsWithTag(chemicalTag);

        float timer = 0f;

        // 점점 사라지게
        while (timer < sprayDuration)
        {
            timer += Time.deltaTime;

            float alpha =
                Mathf.Lerp(1f, 0f, timer / sprayDuration);

            foreach (GameObject chemical in chemicals)
            {
                if (chemical == null) continue;

                SpriteRenderer sr =
                    chemical.GetComponent<SpriteRenderer>();

                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = alpha;
                    sr.color = c;
                }
            }

            yield return null;
        }

        // 물 멈춤
        foreach (ParticleSystem ps in waterParticles)
        {
            if (ps != null)
            {
                ps.Stop();
            }
        }

        // 의약품 삭제
        foreach (GameObject chemical in chemicals)
        {
            if (chemical != null)
            {
                Destroy(chemical);
            }
        }
    }
}