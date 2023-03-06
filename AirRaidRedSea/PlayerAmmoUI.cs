using MyGUI.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRaidRedSea
{
    public class PlayerAmmoUI
    {
        private int ammoNumber;
        private Stack<Widget> imageAmmoStack;
        private StaticText txtAmmoNumber;
        private bool isInited;

        public PlayerAmmoUI() 
        {
            ammoNumber = AmmoManager.Instance.CurrentAmmoNumber;
            AmmoManager.Instance.AmmoChanged += AmmoChanged;
        }

        public void Init()
        {
            if(!isInited)
            {
                imageAmmoStack = new Stack<Widget>();

                int top = 0;
                int left = 0;
                for (int i = 0; i < 10; i++)
                {
                    var imgAmmo = Gui.Instance.CreateWidget<StaticImage>("StaticImage", new IntCoord(left, top, 100, 21), Align.Default, "Main");
                    imgAmmo.SetImageTexture("UI-Ammo.png");

                    if (i == 0)
                    {
                        Helper.SnapToParent(imgAmmo, Align.Right | Align.Bottom);
                        top = imgAmmo.GetTop();
                        left = imgAmmo.GetLeft();
                    }
                    else
                    {
                        top -= imgAmmo.GetHeight();
                    }

                    imageAmmoStack.Push(imgAmmo);
                }

                txtAmmoNumber = Gui.Instance.CreateWidget<StaticText>("StaticText", new IntCoord(left + 10, top - 21, 100, 50), Align.Default, "Main");
                txtAmmoNumber.TextColour = Colour.Green;
                txtAmmoNumber.FontName = "Airal";
                txtAmmoNumber.FontHeight = 30;
                txtAmmoNumber.TextAlign = Align.Center;
                txtAmmoNumber.SetCaption(ammoNumber.ToString());

                isInited = true;
            }
        }

        private void updateUI(AmmoChangeType ammoChangeType)
        {
            switch (ammoChangeType)
            {
                case AmmoChangeType.Add:
                    foreach (StaticImage staticImage in imageAmmoStack)
                    {
                        staticImage.Visible = true;
                    }
                    break;
                case AmmoChangeType.Remove:
                    if (ammoNumber > 10)
                    {
                        if (imageAmmoStack.Where(o => o.Visible).Count() == 0)
                        {
                            foreach (StaticImage staticImage in imageAmmoStack)
                            {
                                staticImage.Visible = true;
                            }
                        }
                        else
                        {
                            foreach (StaticImage staticImage in imageAmmoStack)
                            {
                                if (!staticImage.Visible)
                                    continue;

                                staticImage.Visible = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (StaticImage staticImage in imageAmmoStack)
                        {
                            if (!staticImage.Visible)
                                continue;

                            staticImage.Visible = false;
                            break;
                        }
                    }

                    break;
            }
        }

        private void AmmoChanged(AmmoChangeType ammoChangeType)
        {
            ammoNumber = AmmoManager.Instance.CurrentAmmoNumber;
            txtAmmoNumber.SetCaption(ammoNumber.ToString());

            if(ammoChangeType == AmmoChangeType.Add ||
               ammoChangeType == AmmoChangeType.Remove)
            {
                updateUI(ammoChangeType);
            }
        }
    }
}
