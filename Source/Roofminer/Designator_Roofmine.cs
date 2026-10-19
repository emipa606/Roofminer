using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Roofminer;

public class Designator_Roofmine : Designator_Mine
{
    public Designator_Roofmine()
    {
        defaultLabel = "DesignatorRoofmine".Translate();
        icon = ContentFinder<Texture2D>.Get("Designators/Roofmine");
        defaultDesc = "DesignatorRoofmineDesc".Translate();
    }

    public override DrawStyleCategoryDef DrawStyleCategory => DrawStyleCategoryDefOf.Orders;

    public override void DesignateSingleCell(IntVec3 loc)
    {
        var originalLocRoof = Map.roofGrid.RoofAt(loc);
        // tiles are added to a queue to ensure we process closer tiles first
        var locQueue = new Queue<IntVec3>();
        // remember every tile we have queued, to avoid duplicating effort
        var locQueued = new HashSet<IntVec3>();

        locQueue.Enqueue(loc);
        locQueued.Add(loc);

        var numDesignated = 0;

        while (locQueue.Count > 0 && numDesignated < 1201)
        {
            loc = locQueue.Dequeue();

            Thing locThing = loc.GetFirstMineable(Map);
            if (locThing == null)
            {
                continue;
            }

            if (!CanDesignateThing(locThing).Accepted)
            {
                continue;
            }

            var locRoof = Map.roofGrid.RoofAt(loc);
            if (locRoof == null && originalLocRoof != null ||
                locRoof != null && originalLocRoof == null)
            {
                continue;
            }

            if (locRoof != null &&
                locRoof.defName != originalLocRoof?.defName)
            {
                continue;
            }

            if (Map.designationManager.DesignationAt(loc, DesignationDefOf.Mine) != null)
            {
                continue;
            }

            base.DesignateSingleCell(loc);
            numDesignated++;

            foreach (var newLoc in GenAdjFast.AdjacentCellsCardinal(loc))
            {
                if (!newLoc.InBounds(Map))
                {
                    continue;
                }

                if (locQueued.Contains(newLoc))
                {
                    continue;
                }

                locQueue.Enqueue(newLoc);
                locQueued.Add(newLoc);
            }
        }
    }

    public override void DrawMouseAttachments()
    {
        base.DrawMouseAttachments();
        var mouseCell = UI.MouseCell();
        if (!mouseCell.InBounds(Map) || mouseCell.Fogged(Map))
        {
            return;
        }

        if (mouseCell.GetFirstMineable(Map) == null)
        {
            return;
        }

        var currentRoof = Map.roofGrid.RoofAt(mouseCell);
        var toolTip = currentRoof == null ? "Unroofed" : currentRoof.label.CapitalizeFirst();

        var mousePosition = Event.current.mousePosition;
        var rect = new Rect(mousePosition.x + 29f, mousePosition.y, 999f, 29f);
        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.MiddleLeft;
        Widgets.Label(rect, toolTip);
        Text.Anchor = TextAnchor.UpperLeft;
        GUI.color = Color.white;
    }
}