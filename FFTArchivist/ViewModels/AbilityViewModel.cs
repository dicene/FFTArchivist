using FFTArchivist.Models;
using FFTArchivist.Views.Abilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FFTArchivist.Entries
{
    internal class AbilityViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Ability ability;
        private ActionAbilityViewModel actionAbilityViewModel = new();
        private ItemAbilityViewModel itemAbilityViewModel = new();
        private ThrowAbilityViewModel throwAbilityViewModel = new();
        private JumpAbilityViewModel jumpAbilityViewModel = new();
        private ChargeAbilityViewModel chargeAbilityViewModel = new();
        private MathAbilityViewModel mathAbilityViewModel = new();
        private SupportAbilityViewModel supportAbilityViewModel = new();

        private ActionAbility actionAbility;
        private ItemAbility itemAbility;
        private ThrowAbility throwAbility;
        private JumpAbility jumpAbility;
        private ChargeAbility chargeAbility;
        private MathAbility mathAbility;
        private SupportAbility supportAbility;

        private ActionAbilityView actionAbilityView = new();
        private ItemAbilityView itemAbilityView = new();
        private ThrowAbilityView throwAbilityView = new();
        private JumpAbilityView jumpAbilityView = new();
        private ChargeAbilityView chargeAbilityView = new();
        private MathAbilityView mathAbilityView = new();
        private SupportAbilityView supportAbilityView = new();

        //private System.Windows.Controls.Page CurrentPage;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string secondaryType { get; set; }
        public string AbilityType
        {
            get => secondaryType;
            set
            {
                secondaryType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityType)));
            }
        }

        private Page currentPage { get; set; }
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
                }
            }
        }

        public Ability Ability
        {
            get
            {
                return ability;
            }

            set
            {
                if (ability == value)
                {
                    return;
                }

                ability = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JpCost)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChanceToLearn)));
            }
        }

        //public ActionAbility ActionAbility
        //{
        //    get
        //    {
        //        return actionAbility;
        //    }

        //    set
        //    {
        //        if (actionAbility == value)
        //        {
        //            return;
        //        }

        //        actionAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //    }
        //}

        //public ItemAbility ItemAbility
        //{
        //    get
        //    {
        //        return itemAbility;
        //    }

        //    set
        //    {
        //        if (itemAbility == value)
        //        {
        //            return;
        //        }

        //        itemAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(itemAbility.ItemId)));
        //    }
        //}

        //public ThrowAbility ThrowAbility
        //{
        //    get
        //    {
        //        return throwAbility;
        //    }

        //    set
        //    {
        //        if (throwAbility == value)
        //        {
        //            return;
        //        }

        //        throwAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(throwAbility.ItemId)));
        //    }
        //}

        //public JumpAbility JumpAbility
        //{
        //    get
        //    {
        //        return jumpAbility;
        //    }

        //    set
        //    {
        //        if (jumpAbility == value)
        //        {
        //            return;
        //        }

        //        jumpAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jumpAbility.Range)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(jumpAbility.Vertical)));
        //    }
        //}

        //public ChargeAbility ChargeAbility
        //{
        //    get
        //    {
        //        return chargeAbility;
        //    }

        //    set
        //    {
        //        if (chargeAbility == value)
        //        {
        //            return;
        //        }

        //        chargeAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(chargeAbility.CT)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(chargeAbility.Power)));
        //    }
        //}

        //public MathAbility MathAbility
        //{
        //    get
        //    {
        //        return mathAbility;
        //    }

        //    set
        //    {
        //        if (mathAbility == value)
        //        {
        //            return;
        //        }

        //        mathAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(mathAbility.MathFlags)));
        //        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(mathAbility.Value)));
        //    }
        //}

        //public SupportAbility SupportAbility
        //{
        //    get
        //    {
        //        return supportAbility;
        //    }

        //    set
        //    {
        //        if (supportAbility == value)
        //        {
        //            return;
        //        }

        //        supportAbility = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbilityViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(supportAbility.AbilityId)));
        //    }
        //}

        public int Id { get => ability.Id; set => ability.Id = value; }
        public string Name
        {
            get => ability.Name?.Value ?? "N/A";
            set
            {
                if (ability.Name.Value == value)
                {
                    return;
                }

                ability.Name.Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Description
        {
            get => ability.Description?.Value.Replace("<br>", "\n") ?? "";
            set
            {
                if (ability.Description.Value.Replace("\n", "<br>") == value)
                {
                    return;
                }

                ability.Description.Value = value.Replace("\n", "<br>");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public int JpCost
        {
            get
            {
                return (ability.JpCost1?.Value ?? 0) + ((ability.JpCost2?.Value ?? 0) << 8);
            }

            set
            {
                ability.JpCost1.Value = (byte)(value & 0xff);
                ability.JpCost2.Value = (byte)(value >> 8);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JpCost)));
            }
        }

        public int ChanceToLearn
        {
            get
            {
                return ability.ChanceToLearn?.Value ?? 0;
            }

            set
            {
                ability.ChanceToLearn.Value = (byte)(value & 0xff);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChanceToLearn)));
            }
        }

        public AbilityViewModel()
        {
            ability = new Ability(0);
            actionAbility = new ActionAbility(0);
            itemAbility = new ItemAbility(0);
            throwAbility = new ThrowAbility(0);
            jumpAbility = new JumpAbility(0);
            chargeAbility = new ChargeAbility(0);
            mathAbility = new MathAbility(0);
            supportAbility = new SupportAbility(0);

            actionAbilityView.DataContext = actionAbilityViewModel;
            itemAbilityView.DataContext = itemAbilityViewModel;
            throwAbilityView.DataContext = throwAbilityViewModel;
            jumpAbilityView.DataContext = jumpAbilityViewModel;
            chargeAbilityView.DataContext = chargeAbilityViewModel;
            mathAbilityView.DataContext = mathAbilityViewModel;
            supportAbilityView.DataContext = supportAbilityViewModel;

            actionAbilityViewModel.ActionAbility = actionAbility;
            itemAbilityViewModel.ItemAbility = itemAbility;
            throwAbilityViewModel.ThrowAbility = throwAbility;
            jumpAbilityViewModel.JumpAbility = jumpAbility;
            chargeAbilityViewModel.ChargeAbility = chargeAbility;
            mathAbilityViewModel.MathAbility = mathAbility;
            supportAbilityViewModel.SupportAbility = supportAbility;
        }

        public void ChangeIndex(int index)
        {
            Ability = App.DataManager.GetDataList<Ability>()[index];

            if (index < 0x170)      //Action
            {
                AbilityType = "Action";
                actionAbilityViewModel.ChangeIndex(index);
                CurrentPage = actionAbilityView;
            }
            else if (index < 0x17E) //Item
            {
                AbilityType = "Item";
                itemAbilityViewModel.ChangeIndex(index - 0x170);
                CurrentPage = itemAbilityView;
            }
            else if (index < 0x18A) //Throw
            {
                AbilityType = "Throw";
                throwAbilityViewModel.ChangeIndex(index - 0x17E);
                //ThrowAbility = App.DataManager.GetDataList<ThrowAbility>()[index - 0x17E];
                CurrentPage = throwAbilityView;
            }
            else if (index < 0x196) //Jump
            {
                AbilityType = "Jump";
                jumpAbilityViewModel.ChangeIndex(index - 0x18A);
                //JumpAbility = App.DataManager.GetDataList<JumpAbility>()[index - 0x18A];
                CurrentPage = jumpAbilityView;
            }
            else if (index < 0x19E) //Charge
            {
                AbilityType = "Charge";
                chargeAbilityViewModel.ChangeIndex(index - 0x196);
                //ChargeAbility = App.DataManager.GetDataList<ChargeAbility>()[index - 0x196];
                CurrentPage = chargeAbilityView;
            }
            else if (index < 0x1A6) //Math
            {
                AbilityType = "Math";
                mathAbilityViewModel.ChangeIndex(index - 0x19E);
                //MathAbility = App.DataManager.GetDataList<MathAbility>()[index - 0x19E];
                CurrentPage = mathAbilityView;
            }
            else if (index < 0x200) //RSM
            {
                AbilityType = "RSM";
                supportAbilityViewModel.ChangeIndex(index - 0x1A6);
                //SupportAbility = App.DataManager.GetDataList<SupportAbility>()[index - 0x1A6];
                CurrentPage = supportAbilityView;
            }
        }
    }
}
