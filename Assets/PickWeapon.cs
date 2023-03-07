using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UnityEngine.Localization
{
    public class PickWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool mouseOver;
        public bool isResetButton;
        Move playerscript;
        UIUpdater ui;

        public string Weapon;

        float scaleUp;
        public float scaleMultiplier;
        // Update is called once per frame
        private void Start()
        {
            playerscript = GameObject.Find("Player").GetComponent<Move>();
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
            playerscript.disableInput = true;
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().DisableSpawning();

        }
        void Update()
        {
            if (mouseOver)
            {
                if (scaleUp < 0.2f)
                {
                    transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 1) * (1 + scaleUp), 1.0f);
                    scaleUp += 1f * Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    SetWeapon();
                }
            }
            if (!mouseOver)
            {
                if (scaleUp > 0f)
                {
                    transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 1) * (1 + scaleUp), 1.0f);
                    scaleUp -= 1f * Time.deltaTime;
                }
            }
        }
        void SetWeapon()
        {
            playerscript.EquippedWeapon = Weapon;
            ui.PickWeapon();
            ui.transform.Find("WeaponOptions").gameObject.SetActive(false);
            ui.transform.Find("FadeIn").gameObject.GetComponent<FadeIn>().fadeMe = true;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOver = false;
        }
    }
}
