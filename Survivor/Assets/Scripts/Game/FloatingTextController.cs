using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class FloatingTextController : ViewController
    {
        void Start()
        {
            FloatingText.Hide();
        }

        public static void Play(Vector2 position, string text,bool critical = false)
        {
            mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
                .Position(position.x, position.y)
                .Self(f =>
                {
                    var positionY = position.y;
                    var textTrans = f.transform.Find("Text");
                    var textComp = textTrans.GetComponent<Text>();
                    textComp.text = text;

                    if (critical)
                    {
                        textComp.color = Color.red;
                    }


                    ActionKit.Sequence()
                        .Lerp(0, 0.5f, 0.5f, (p) =>
                        {
                            f.PositionY(positionY + p * 0.25f);
                            textComp.LocalScaleX(Mathf.Clamp01(p * 8));
                            textComp.LocalScaleY(Mathf.Clamp01(p * 8));
                        })
                        .Delay(0.5f)
                        .Lerp(1.0f, 0, 0.3f, (p) => { textComp.ColorAlpha(p); },
                            () => { textTrans.DestroyGameObjGracefully(); })
                        .Start(textComp);
                }).Show();
        }

        private static FloatingTextController mDefault;

        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            mDefault = null;
        }
    }
}