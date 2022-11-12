using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using System.Linq;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class DRGCombo_Base<TCmd> : JobGaugeCombo<DRGGauge, TCmd> where TCmd : Enum
{
    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Dragoon, ClassJobID.Lancer };
    private static bool safeMove = false;

    public static readonly BaseAction
        //��׼��
        TrueThrust = new(75),

        //��ͨ��
        VorpalThrust = new(78) { OtherIDsCombo = new[] { 16479u } },

        //ֱ��
        FullThrust = new(84),

        //����
        HeavensThrust = new(25771),

        //����ǹ
        Disembowel = new(87) { OtherIDsCombo = new[] { 16479u } },

        //ӣ��ŭ��
        ChaosThrust = new(ActionIDs.ChaosThrust),

        //ӣ��ŭ��
        ChaoticSpring = new(25772),

        //������צ
        FangandClaw = new(ActionIDs.FangandClaw)
        {
            BuffsNeed = new StatusID[] { StatusID.SharperFangandClaw },
        },

        //��β�����
        WheelingThrust = new(ActionIDs.WheelingThrust)
        {
            BuffsNeed = new StatusID[] { StatusID.EnhancedWheelingThrust },
        },

        //�����׵�
        RaidenThrust = new(16479),

        //�ᴩ��
        PiercingTalon = new(90),

        //����ǹ
        DoomSpike = new(86),

        //���ٴ�
        SonicThrust = new(7397) { OtherIDsCombo = new[] { 25770u } },

        //ɽ������
        CoerthanTorment = new(16477),

        //�����
        SpineshatterDive = new(95)
        {
            OtherCheck = b =>
            {
                if (safeMove && b.DistanceToPlayer() > 2) return false;
                if (IsLastAction(true, SpineshatterDive)) return false;

                return true;
            }
        },

        //���׳�
        DragonfireDive = new(96)
        {
            OtherCheck = b => !safeMove || b.DistanceToPlayer() < 2,
        },

        //��Ծ
        Jump = new(92)
        {
            BuffsProvide = new StatusID[] { StatusID.DiveReady },
            OtherCheck = b => (!safeMove || b.DistanceToPlayer() < 2) && Player.HaveStatus(StatusID.PowerSurge),
        },
        //����
        HighJump = new(16478)
        {
            OtherCheck = Jump.OtherCheck,
        },
        //�����
        MirageDive = new(7399)
        {
            BuffsNeed = new[] { StatusID.DiveReady },

            OtherCheck = b => !Geirskogul.WillHaveOneChargeGCD(4)
        },

        //����ǹ
        Geirskogul = new(3555)
        {
            OtherCheck = b => Jump.IsCoolDown || HighJump.IsCoolDown,
        },

        //����֮��
        Nastrond = new(7400)
        {
            OtherCheck = b => JobGauge.IsLOTDActive,
        },

        //׹�ǳ�
        Stardiver = new(16480)
        {
            OtherCheck = b => JobGauge.IsLOTDActive && JobGauge.LOTDTimer < 25000,
        },

        //�����㾦
        WyrmwindThrust = new(25773)
        {
            OtherCheck = b => JobGauge.FirstmindsFocusCount == 2 && !IsLastAction(true, Stardiver),
        },

        //����
        LifeSurge = new(83)
        {
            BuffsProvide = new[] { StatusID.LifeSurge },

            OtherCheck = b => !IsLastAbility(true, LifeSurge),
        },

        //��ǹ
        LanceCharge = new(85),

        //��������
        DragonSight = new(7398)
        {
            ChoiceTarget = Targets =>
            {
                Targets = Targets.Where(b => b.ObjectId != Service.ClientState.LocalPlayer.ObjectId &&
                !b.HaveStatus(StatusID.Weakness, StatusID.BrinkofDeath)).ToArray();

                var targets = TargetFilter.GetJobCategory(Targets, Role.��ս);
                if (targets.Length > 0) return TargetFilter.RandomObject(targets);

                targets = TargetFilter.GetJobCategory(Targets, Role.Զ��);
                if (targets.Length > 0) return TargetFilter.RandomObject(targets);

                targets = Targets;
                if (targets.Length > 0) return TargetFilter.RandomObject(targets);

                return Player;
            },

            BuffsNeed = new[] { StatusID.PowerSurge },

        },

        //ս������
        BattleLitany = new(3557)
        {
            BuffsNeed = new[] { StatusID.PowerSurge },
        };

}