using System.Collections.Generic;
using RimWorld;
using Verse;
using UnityEngine;

namespace Roofminer
{
    public class Designator_Roofmine : Designator_Mine
    {

        public override int DraggableDimensions => 0;

        public Designator_Roofmine()
        {
            defaultLabel = "DesignatorRoofmine".Translate();
            icon = ContentFinder<Texture2D>.Get("Designators/Roofmine", true);
            defaultDesc = "DesignatorRoofmineDesc".Translate();
        }

        public override void DesignateSingleCell(IntVec3 loc)
        {

            RoofDef originalLocRoof = Map.roofGrid.RoofAt(loc);
            // tiles are added to a queue to ensure we process closer tiles first
            Queue<IntVec3> locQueue = new Queue<IntVec3>();
            // remember every tile we have queued, to avoid duplicating effort
            HashSet<IntVec3> locQueued = new HashSet<IntVec3>();

            locQueue.Enqueue(loc);
            locQueued.Add(loc);

            int numDesignated = 0;

            while (locQueue.Count > 0 && numDesignated < 1201)
            {

                // Log.Message("Deqeueing " + loc.ToString());
                loc = locQueue.Dequeue();

                // Log.Message("getting locThing");
                Thing locThing = loc.GetFirstMineable(Map);
                // Log.Message("checking locThing==null");
                if (locThing == null)
                    continue;
                // Log.Message("checking CanDesignateThing");
                if (!CanDesignateThing(locThing).Accepted)
                    continue;
                // Log.Message("getting locRoof");
                RoofDef locRoof = Map.roofGrid.RoofAt(loc);
                // Log.Message("checking one null roof");
                if (locRoof == null && originalLocRoof != null ||
                        locRoof != null && originalLocRoof == null)
                    continue;
                // Log.Message("checking roof defNames match");
                if (locRoof != null && originalLocRoof != null &&
                        locRoof.defName != originalLocRoof.defName)
                    continue;
                // Log.Message("checking DesignationAt");
                if (Map.designationManager.DesignationAt(loc, DesignationDefOf.Mine) != null)
                    continue;

                // Log.Message("Designating " + loc.ToString());
                base.DesignateSingleCell(loc);
                numDesignated++;

                foreach (IntVec3 newLoc in GenAdjFast.AdjacentCellsCardinal(loc))
                {
                    if (!newLoc.InBounds(Map))
                        continue;
                    if (!locQueued.Contains(newLoc))
                    {
                        // Log.Message("Enqueueing " + newLoc.ToString());
                        locQueue.Enqueue(newLoc);
                        locQueued.Add(newLoc);
                    }
                }

            }
        }

        public override void DrawMouseAttachments()
        {
            base.DrawMouseAttachments();
            var mouseCell = UI.MouseCell();
            if (mouseCell == null || !mouseCell.InBounds(Map) || mouseCell.Fogged(Map))
            {
                return;
            }
            if (mouseCell.GetFirstMineable(Map) == null)
            {
                return;
            }
            var currentRoof = Map.roofGrid.RoofAt(mouseCell);
            string toolTip;
            if (currentRoof == null)
            {
                toolTip = "Unroofed";
            }
            else
            {
                toolTip = currentRoof.label.CapitalizeFirst();
            }
            var mousePosition = Event.current.mousePosition;
           Rect rect = new Rect(mousePosition.x + 29f, mousePosition.y, 999f, 29f);
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect, toolTip);
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;
        }
    }
}
