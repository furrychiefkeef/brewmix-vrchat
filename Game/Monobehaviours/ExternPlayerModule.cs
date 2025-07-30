using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using brewchat.hybridxcore.bep.Game.Monobehaviours;
using UnityEngine;
using VRC.Core;

namespace autumn.ml.Game.Monobehaviours
{
    public class ExternPlayerModule : MonoBehaviour
    {
        public ExternPlayerModule(IntPtr ptr) : base(ptr) { }
        public enum ModeratorStatus
        {
            None,
            Agent,
            Admin,
            Moderator,
        }
        public VRC.Player? Player { get; set; }
        public Nameplates Nameplate { get; set; }
        public ModeratorStatus moderatorStatus = ModeratorStatus.None;
        #region Good Actors
        public bool isSpecialUser = false;
        public bool isOG = false;
        public bool isLexie = false;
        public bool isAgent = false;
        #endregion


        #region Bad Actors

        public bool isPedophile = false;
        public bool isDirtyCheater = false;
        public bool isBlacklisted = false;
        #endregion
    }
}
