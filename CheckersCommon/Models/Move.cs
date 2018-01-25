using System;
using Newtonsoft.Json;

namespace CheckersCommon.Models
{
    public sealed class Move
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "destinated_position")]
        public Position DestinatedPosition { get; set; }

        [JsonIgnore]
        public Position SourcePosition { get; set; }

        [JsonIgnore]
        public Position MiddlePosition
        {
            get
            {
                if (!IsCapture)
                {
                    throw new InvalidOperationException("Move doesn't capture");

                }

                int row = SourcePosition.Row - (SourcePosition.Row - DestinatedPosition.Row) / 2;
                int column = SourcePosition.Column - (SourcePosition.Column - DestinatedPosition.Column) / 2;

                return new Position { Row = row, Column = column };
            }
        }

        [JsonIgnore]
        public bool IsCapture
        {
            get
            {
                return Math.Abs(SourcePosition.Row - DestinatedPosition.Row) > 1 || Math.Abs(SourcePosition.Column - DestinatedPosition.Column) > 1;
            }
        }
    }
}
