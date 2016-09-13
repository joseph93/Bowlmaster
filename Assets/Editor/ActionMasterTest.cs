using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster actionMaster;

    [SetUp]
    public void SetUp()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl0To9()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03SecondBowlEndTurn()
    {
        actionMaster.Bowl(4);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04BowlASpare()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T05FirstTurnScoreAfterOneStrike()
    {
        actionMaster.Bowl(10);
        actionMaster.Bowl(5);
        actionMaster.Bowl(2);
        int[] bowls = actionMaster.getBowls();
        Assert.AreEqual(17, bowls[0]);
    }

    [Test]
    public void T06FirstTurnScoreAfterASpare()
    {
        actionMaster.Bowl(8);
        actionMaster.Bowl(2);
        actionMaster.Bowl(5);
        int[] bowls = actionMaster.getBowls();
        Assert.AreEqual(15, bowls[0] + bowls[1]);
    }

    [Test]
    public void T07ScoreAfterTwoStrikesInARow()
    {
        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        actionMaster.Bowl(5);
        actionMaster.Bowl(5);
        int[] bowls = actionMaster.getBowls();
        Assert.AreEqual(20, bowls[0]);
        Assert.AreEqual(20, bowls[2]);
    }

    [Test]
    public void T08ScoreAfterOneSpareAndAStrike()
    {
        actionMaster.Bowl(7);
        actionMaster.Bowl(3);
        actionMaster.Bowl(10);
    }
}

