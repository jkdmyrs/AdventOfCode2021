namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day16 : BasePuzzle
    {
        private Packet _packet;

        private enum PacketType
        {
            Literal,
            Operator
        }
        private enum LengthType
        {
            None,
            LengthInBits,
            NumberOfSubPackets
        }

        private enum OperatorType
        {
            None,
            Sum,
            Product,
            Minimum,
            Maximum,
            GreaterThan,
            LessThan,
            EqualTo
        }

        private static int BinaryToInt(List<char> binary)
        {
            int exponent = binary.Count() - 1;
            int decimalValue = 0;
            binary.ForEach(val =>
            {
                if (val == '1')
                {
                    decimalValue += (int)Math.Pow(2, exponent);
                }
                exponent--;
            });
            return decimalValue;
        }

        private static long BinaryToLong(List<char> binary)
        {
            int exponent = binary.Count() - 1;
            long decimalValue = 0;
            binary.ForEach(val =>
            {
                if (val == '1')
                {
                    decimalValue += (long)Math.Pow(2, exponent);
                }
                exponent--;
            });
            return decimalValue;
        }

        private class Packet
        {
            public Packet(string bits)
            {
                _bits = bits;
                if (this.PacketType == PacketType.Operator)
                {
                    int index = 7;
                    if (this.LengthType == LengthType.LengthInBits)
                    {
                        int lengthLimit = BinaryToInt(_bits.Substring(index, 15).ToList());
                        index += 15;
                        int subPacketLength = 0;
                        while (subPacketLength < lengthLimit)
                        {
                            this.Packets.Add(new Packet(_bits.Substring(index, GetPacketLength(_bits.Substring(index)))));
                            index += this.Packets.Last().Bits.Length;
                            subPacketLength += this.Packets.Last().Bits.Length;
                        }
                    }
                    else
                    {
                        int numPackets = BinaryToInt(_bits.Substring(index, 11).ToList());
                        int packetsCreated = 0;
                        index += 11;
                        while (packetsCreated < numPackets)
                        {
                            this.Packets.Add(new Packet(_bits.Substring(index, GetPacketLength(_bits.Substring(index)))));
                            index += this.Packets.Last().Bits.Length;
                            packetsCreated++;
                        }
                    }
                }
            }

            public int GetPacketLength(string packetBits)
            {
                int packetLength = 6;
                if (GetPacketType(packetBits.Substring(0, 7)) == PacketType.Literal)
                {
                    int temp;
                    do
                    {
                        temp = packetLength;
                        packetLength += 5;
                    } while (packetBits[temp] == '1');
                    return packetLength;
                }
                else
                {
                    packetLength += 1;
                    if (GetLengthType(packetBits.Substring(0, 7)) == LengthType.LengthInBits)
                    {
                        int lenth = BinaryToInt(packetBits.Substring(packetLength, 15).ToList());
                        packetLength += 15;
                        packetLength += lenth;
                        return packetLength;
                    }
                    else
                    {
                        int numSubPackets = BinaryToInt(packetBits.Substring(packetLength, 11).ToList());
                        packetLength += 11;
                        int newSubPacketCounter = 0;
                        while (newSubPacketCounter < numSubPackets)
                        {
                            packetLength += GetPacketLength(packetBits.Substring(packetLength));
                            newSubPacketCounter++;
                        };
                        return packetLength;
                    }
                }
            }


            private static PacketType GetPacketType(string msgWithHeader)
            {
                return BinaryToInt(msgWithHeader.Substring(3, 3).ToList()) == 4 ? PacketType.Literal : PacketType.Operator;
            }

            private static LengthType GetLengthType(string msgWithHeader)
            {
                return GetPacketType(msgWithHeader) == PacketType.Literal ? LengthType.None : msgWithHeader.Substring(6, 1) == "0" ? LengthType.LengthInBits : LengthType.NumberOfSubPackets;
            }

            public PacketType PacketType => GetPacketType(this.Bits);

            public OperatorType OperatorType
            {
                get
                {
                    if (this.PacketType == PacketType.Literal) return OperatorType.None;
                    switch(BinaryToInt(this.Bits.Substring(3,3).ToList()))
                    {
                        case 0:
                            return OperatorType.Sum;
                        case 1:
                            return OperatorType.Product;
                        case 2:
                            return OperatorType.Minimum;
                        case 3:
                            return OperatorType.Maximum;
                        case 5:
                            return OperatorType.GreaterThan;
                        case 6:
                            return OperatorType.LessThan;
                        case 7:
                            return OperatorType.EqualTo;
                    }
                    throw new Exception("no op type");
                }
            }

            private string _bits;
            public string Bits
            {
                get
                {
                    int length = GetPacketLength(_bits) + (4 - (4 % GetPacketLength(_bits)));
                    return _bits.Substring(0, length);
                }
            }
            public int Version => BinaryToInt(this.Bits.Substring(0, 3).ToList());
            public long Value
            {
                get
                {
                    long value;
                    if (this.PacketType == PacketType.Operator)
                    {
                        switch (this.OperatorType)
                        {
                            case OperatorType.Sum:
                                value = this.Packets.Sum(x => x.Value);
                                break;
                            case OperatorType.Product:
                                long product = 1;
                                this.Packets.ForEach(packet =>
                                {
                                    product = packet.Value * product;
                                });
                                value = product;
                                break;
                            case OperatorType.Minimum:
                                value = this.Packets.Min(x => x.Value);
                                break;
                            case OperatorType.Maximum:
                                value = this.Packets.Max(x => x.Value);
                                break;
                            case OperatorType.GreaterThan:
                                value = this.Packets[0].Value > this.Packets[1].Value ? 1 : 0;
                                break;
                            case OperatorType.LessThan:
                                value = this.Packets[0].Value < this.Packets[1].Value ? 1 : 0;
                                break;
                            case OperatorType.EqualTo:
                                value = this.Packets[0].Value == this.Packets[1].Value ? 1 : 0;
                                break;
                            default:
                                throw new Exception("invalid");
                        }
                    }
                    else
                    {
                        int i = 6;
                        int temp;
                        string number = string.Empty;
                        do
                        {
                            temp = i;
                            number += this.Bits.Substring(i + 1, 4);
                            i += 5;

                        } while (this.Bits[temp] == '1');
                        value = BinaryToLong(number.ToList());
                    }

                    if (value < 0)
                    {
                        throw new Exception("overflow?");
                    }
                    return value;
                }
            }
            public LengthType LengthType => GetLengthType(this.Bits);
            public List<Packet> Packets { get; set; } = new();
        }

        private static IEnumerable<Packet> Flatten(Packet packet)
        {
            yield return packet;
            if (packet.Packets != null)
            {
                foreach (var child in packet.Packets)
                    foreach (var descendant in Flatten(child))
                        yield return descendant;
            }
        }

        private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

        public string HexStringToBinary(string hex)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in hex)
            {
                result.Append(hexCharacterToBinary[c]);
            }
            return result.ToString();
        }

        public Day16(bool practice)
            : base(16, practice)
        {
            string hexMessage = _puzzleInput.First();
            string binaryMessage = HexStringToBinary(hexMessage);
            _packet = new Packet(binaryMessage);
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        public int Part1()
        {
            var packets = Flatten(_packet);
            return packets.ToList().Sum(x => x.Version);
        }

        public long Part2()
        {
            return _packet.Value;
        }
    }
}
