using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class Session
    {
        public Session(DictaatSession session)
        {
            this.ParticipantIds = session.Participants.Select(p => p.UserId);
        }

        public bool ContainsMe { get; internal set; }
        public IEnumerable<string> ParticipantIds { get; private set; }
    }
}
