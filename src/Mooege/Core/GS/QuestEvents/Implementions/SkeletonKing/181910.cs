﻿/*
 * Copyright (C) 2011 - 2018 mooege project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mooege.Common.MPQ.FileFormats;
using Mooege.Net.GS.Message.Definitions.ACD;
using Mooege.Net.GS.Message.Definitions.Animation;
using Mooege.Core.GS.Common.Types.Math;
using Mooege.Core.GS.Generators;
using Mooege.Common.Logging;
using System.Threading.Tasks;
using System.Threading;
using Mooege.Core.GS.Map;
using Mooege.Core.GS.Common.Types.TagMap;
using Mooege.Common.Storage;
using Mooege.Common.Storage.AccountDataBase.Entities;
using Mooege.Core.GS.Actors.Interactions;

namespace Mooege.Core.GS.QuestEvents.Implementations
{
    class _181910 : QuestEvent
    {

        private static readonly Logger Logger = LogManager.CreateLogger();
        public List<ConversationInteraction> Conversations { get; private set; }

        public _181910()
            : base(181910)
        {
        }

        public override void Execute(Map.World world)
        {
            Logger.Debug(" Conversation done ");

         
            // starting second conv
            var wrongNPC = world.GetActorBySNO(180900);
            world.SpawnMonster(117365, wrongNPC.Position);
            var RightNPC = world.GetActorBySNO(117365);
            world.Leave(wrongNPC);
            // Idle - 141437
            // 141674-141541-141795-141675
            world.BroadcastIfRevealed(new Mooege.Net.GS.Message.Definitions.Animation.PlayAnimationMessage
            {
                ActorID = RightNPC.DynamicID,
                Field1 = 5,
                Field2 = 0,
                tAnim = new Net.GS.Message.Fields.PlayAnimationMessageSpec[]
                        {
                            new Net.GS.Message.Fields.PlayAnimationMessageSpec()
                            {
                                Duration = 100,
                                AnimationSNO = 141674,
                                PermutationIndex = 0,
                                Speed = 1f
                            }
                        }
            }, RightNPC);
            world.BroadcastIfRevealed(new Mooege.Net.GS.Message.Definitions.Animation.PlayAnimationMessage
            {
                ActorID = RightNPC.DynamicID,
                Field1 = 5,
                Field2 = 0,
                tAnim = new Net.GS.Message.Fields.PlayAnimationMessageSpec[]
                        {
                            new Net.GS.Message.Fields.PlayAnimationMessageSpec()
                            {
                                Duration = 150,
                                AnimationSNO = 141541,
                                PermutationIndex = 0,
                                Speed = 1f
                            }
                        }
            }, RightNPC);
            
            StartConversation(world, 181912);

        }

        //Launch Conversations.
        private bool StartConversation(Map.World world, Int32 conversationId)
        {
            foreach (var player in world.Players)
            {
                player.Value.Conversations.StartConversation(conversationId); // this does the job of sending the proper stuff :p
            }
            return true;
        }
    }
}