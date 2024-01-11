namespace NTP_txtRPG_seven
{
    public class Items
    {

        public int Id { get; }
        public int ItemType { get; }
        public string Name { get; }
        public string Desc { get; }
        public int Price { get; }
        public int Atk { get; }
        public int Def { get; }
        public int HP { get; }
        public int ResellPrice { get; }
        public bool IsEquip { get; set; }
        public bool AlreadyHave { get; set; }
        public bool IsOnSale { get; set; }
        public static int ShopItemCnt = 0;
        public static int itemCnt = 0;


        public Items(int id, int itemType, string name, string desc, int price, int atk, int def, int hp, int resellPrice, bool isEquip, bool alreadyHave, bool isOnSale)
        {
            Id = id;
            ItemType = itemType;
            Name = name;
            Desc = desc;
            Price = price;
            Atk = atk;
            Def = def;
            HP = hp;
            ResellPrice = resellPrice;
            IsEquip = isEquip;
            AlreadyHave = alreadyHave;
            IsOnSale = isOnSale;
        }

        public void PrintItemAbout(bool withNumber = false, int idx = 0)
        {

            Console.Write("-");
            if (withNumber)
            {
                Console.Write("{0}", idx);
            }
            if (IsEquip)
            {
                Console.Write("[E]");
            }
            Console.Write(Name);
            Console.Write("   |   ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk}");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def}");
            if (HP != 0) Console.Write($"HP {(HP >= 0 ? "+" : "")}{HP}");

            Console.Write("   |   ");
            Console.WriteLine(Desc);
        }

        public void ItemShopAbout(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.Write("{0}|", idx);
            }

            if (AlreadyHave)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[구매완료]");
                Console.ResetColor();
            }
            else if (!AlreadyHave)
            {
                Console.Write(Price + "G  |");
            }
            Console.Write(Name);
            Console.Write("   |   ");
            Console.WriteLine(Desc);
        }

        public void ItemSellAbout(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.Write("{0}|", idx);
            }
            Console.Write(ResellPrice + "G  |");
            Console.Write(Name);
            Console.Write("   |   ");
            Console.WriteLine(Desc);
        }
    }


    public class Player
    {
        public int Level { get; }
        public string Name { get; }
        public string Job { get; }
        public int Atk { get; }
        public int Def { get; }
        public int HP { get; set; }
        public int Gold { get; set; }


        public Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            HP = hp;
            Gold = gold;


        }
        class Program()
        {
            static Player _player;
            static Items[] _items;
            static Items[] _haveItems;
            static List<Items> _hands;
            static List<Items> _body;
            static List<Items> _boots;
            static List<Items> _trinket;
            static List<Items> _head;

            private static void gameSetting()
            {
                _player = new Player(1, "까냥꾼", "전사", 10, 5, 100, 1500);
                _items = new Items[8];
                AddShopItem(new Items(1, 1, "연장점검", "세상에서 가장 긴 검.", 150, 120, 0, 0, 128, false, false, true));
                AddShopItem(new Items(2, 1, "긴급점검", "딱히 빠르지는 않다.", 100, 80, 0, 0, 85, false, false, true));
                AddShopItem(new Items(3, 2, "고블린가죽갑옷", "냄새가 좀 난다.", 50, 0, 15, 20, 43, false, false, true));
                AddShopItem(new Items(4, 3, "토끼털슬리퍼", "작게'토끼공듀'라고 적혀있다.", 200, 10, 0, 10, 170, false, false, true));
                AddShopItem(new Items(5, 4, "징크스부적", "불행을 막아주기는 커녕 몰고온다.", 350, 50, -30, 0, 298, false, false, true));
                AddShopItem(new Items(6, 4, "함정카드", "전속전진", 250, 5, 20, 0, 213, false, false, true));
                AddShopItem(new Items(7, 4, "케이크", "블랙포레스트 체리 케이크. 거짓말이다.", 50, 0, 0, 100, 43, false, false, true));
                AddShopItem(new Items(8, 5, "맹독해골", "투구다. 팬티가 아니다.", 500, 0, 20, 60, 425, false, false, true));
                _haveItems = new Items[10];
                AddItem(new Items(9, 1, "주머니칼", "과일 깎아 먹으려고 사뒀던 짧은 나이프.", 0, 10, 0, 0, 0, true, true, false));
                _hands = new List<Items> { _haveItems[0] };
                _body = new List<Items> { };
                _boots = new List<Items> { };
                _trinket = new List<Items> { };
                _head = new List<Items> { };


            }



            static void AddShopItem(Items item)
            {
                if (Items.ShopItemCnt == 10) return;
                _items[Items.ShopItemCnt] = item;
                Items.ShopItemCnt++;
            }
            static void AddItem(Items item)
            {
                if (Items.itemCnt == 10) return;
                _haveItems[Items.itemCnt] = item;
                Items.itemCnt++;
            }
            static void RemoveItem(Items item)
            {
                if (Items.itemCnt == 0) return;
                _haveItems[Items.itemCnt] = item;
                Items.itemCnt--;

            }

            static void Main(string[] args)
            {
                gameSetting();
                PrintStartLogo();
                Start();

            }
            private static int CheckInput(int min, int max)
            {
                int keyInput;
                bool result;
                do
                {
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    result = int.TryParse(Console.ReadLine(), out keyInput);

                } while (result == false || CheckIfValid(keyInput, min, max) == false);
                return keyInput;
            }

            private static bool CheckIfValid(int keyInput, int min, int max)
            {
                if (min <= keyInput && keyInput <= max) return true;
                return false;
            }

            private static void Start()
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1.상태보기");
                Console.WriteLine("2.인벤토리");
                Console.WriteLine("3.상점");
                Console.WriteLine("4.던전입장");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요 >>");

                switch (CheckInput(1, 4))
                {
                    case 1:
                        Status();
                        break;
                    case 2:
                        Inventory();
                        break;
                    case 3:
                        Shop();
                        break;
                    case 4:
                        Dungeon();
                        break;
                }
            }

            private static void Dungeon()
            {
                Console.Clear();
                Console.WriteLine("던전 입장");
                Console.WriteLine("이 곳에서 던전으로 들어가기 전 활동을 하실 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("1.쉬운 던전  |  방어력 5 이상 권장");
                Console.WriteLine("2.일반 던전  |  방어력 11 이상 권장");
                Console.WriteLine("3.어려운 던전  |  방어력 17 이상 권장");
                Console.WriteLine("0.나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요 >>");
                switch (CheckInput(0, 3))
                {
                    case 0:
                        Start();
                        break;
                    case 1:
                        Easy();
                        break;
                    case 2:
                        Normal();
                        break;
                    case 3:
                        Hard();
                        break;
                }

            }

            private static void GameOver()
            {
                _player.HP = 100;
                if (_player.Gold <= 0)
                {
                    _player.Gold = 0;
                }
                else
                {
                    _player.Gold -= 500;
                }
                Console.Clear();
                Console.WriteLine("게임 오버");
                Console.WriteLine("");
                Console.WriteLine("준비를 철저히 합시다.");
                Console.WriteLine("0.다시하기");
                switch (CheckInput(0, 0))
                {
                    case 0:
                        Start();
                        break;
                }
            }

            private static void Hard()
            {
                int bonusDef = getSumBonusDef();
                int bonusHP = getSumBonusHP();
                int bonusAtk = getSumBonusAtk();
                Console.Clear();
                Console.WriteLine("어려운 던전");
                Console.WriteLine("");

                if ((_player.Def + bonusDef) >= 17)
                {
                    _player.HP -= 80;

                    if (_player.HP + bonusHP > 0)
                    {
                        Console.WriteLine("축하합니다!");
                        Console.WriteLine("어려운 던전을 클리어하였습니다.");
                        Console.WriteLine("");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine("체력 : " + (_player.HP + 80) + " -> " + (_player.HP));
                        Console.WriteLine("소지 골드 : " + (_player.Gold) + "G -> " + (_player.Gold += 3500) + "G");
                        Console.ReadKey();
                        Dungeon();
                    }
                    else
                    {
                        GameOver();
                    }
                }

                else
                {
                    GameOver();
                }
            }

            private static void Normal()
            {
                int bonusDef = getSumBonusDef();
                int bonusHP = getSumBonusHP();
                int bonusAtk = getSumBonusAtk();
                Console.Clear();
                Console.WriteLine("일반 던전");
                Console.WriteLine("");

                if ((_player.Def + bonusDef) >= 11)
                {
                    _player.HP -= 50;

                    if (_player.HP + bonusHP > 0)
                    {
                        Console.WriteLine("축하합니다!");
                        Console.WriteLine("일반 던전을 클리어하였습니다.");
                        Console.WriteLine("");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine("체력 : " + (_player.HP + 50) + " -> " + (_player.HP));
                        Console.WriteLine("소지 골드 : " + (_player.Gold) + "G -> " + (_player.Gold += 2000) + "G");
                        Console.ReadKey();
                        Dungeon();
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else
                {
                    GameOver();
                }
            }

            private static void Easy()
            {
                int bonusDef = getSumBonusDef();
                int bonusHP = getSumBonusHP();
                int bonusAtk = getSumBonusAtk();
                Console.Clear();
                Console.WriteLine("쉬운 던전");
                Console.WriteLine("");

                if (_player.Def + bonusDef >= 5)
                {
                    _player.HP -= 30;

                    if (_player.HP + bonusHP > 0)
                    {
                        Console.WriteLine("축하합니다!");
                        Console.WriteLine("쉬운 던전을 클리어하였습니다.");
                        Console.WriteLine("");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine("체력 : " + (_player.HP + 30) + " -> " + (_player.HP));
                        Console.WriteLine("소지 골드 : " + (_player.Gold) + "G -> " + (_player.Gold += 1200) + "G");
                        Console.ReadKey();
                        Dungeon();
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else
                {
                    GameOver();
                }
            }

            private static void Status()
            {

                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine("");
                Console.WriteLine("레벨 : " + _player.Level.ToString("00"));
                Console.WriteLine(_player.Name + "(" + _player.Job + ")");
                int bonusAtk = getSumBonusAtk();
                Console.WriteLine("공격력 : " + (_player.Atk + bonusAtk).ToString(), bonusAtk == 0 ? "" : string.Format("(+{0})", bonusAtk));
                int bonusDef = getSumBonusDef();
                Console.WriteLine("방어력 : " + (_player.Def + bonusDef).ToString(), bonusDef == 0 ? "" : string.Format("(+{0})", bonusDef));
                int bonusHP = getSumBonusHP();
                Console.WriteLine("체력 : " + (_player.HP + bonusHP).ToString(), bonusHP == 0 ? "" : string.Format("(+{0}", bonusHP));
                Console.WriteLine("소지골드 : " + _player.Gold + "G");
                Console.WriteLine("");
                Console.WriteLine("0.뒤로가기");
                Console.WriteLine("");

                switch (CheckInput(0, 0))
                {
                    case 0:
                        Start();
                        break;
                }

            }
            private static int getSumBonusAtk()
            {
                int sum = 0;
                for (int i = 0; i < Items.itemCnt; i++)
                {
                    if (_haveItems[i].IsEquip) sum += _haveItems[i].Atk;
                }
                return sum;
            }
            private static int getSumBonusDef()
            {
                int sum = 0;
                for (int i = 0; i < Items.itemCnt; i++)
                {
                    if (_haveItems[i].IsEquip) sum += _haveItems[i].Def;
                }
                return sum;
            }
            private static int getSumBonusHP()
            {
                int sum = 0;
                for (int i = 0; i < Items.itemCnt; i++)
                {
                    if (_haveItems[i].IsEquip) sum += _haveItems[i].HP;
                }
                return sum;
            }
            private static void Inventory()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");

                if (_haveItems.Length <= 0)
                {
                    Console.WriteLine("아이템이 없습니다.");

                }
                else
                {
                    for (int i = 0; i < Items.itemCnt; i++)
                    {
                        _haveItems[i].PrintItemAbout();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0.뒤로가기");
                Console.WriteLine("1.장착 관리");
                switch (CheckInput(0, 1))
                {
                    case 0:
                        Start();
                        break;
                    case 1:
                        EquipMenu();
                        break;
                }

            }

            private static void EquipMenu() //아이템을 교체할 수 있지만 그냥 벗는 것은 불가능...
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Items.itemCnt; i++)
                {
                    _haveItems[i].PrintItemAbout(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0.나가기");
                int keyInput = CheckInput(0, Items.itemCnt);
                switch (keyInput)
                {
                    case 0:
                        Inventory();
                        break;
                    default:
                        ToggleEquipStatus(keyInput - 1);
                        EquipMenu();
                        break;
                }
            }

            private static void ToggleEquipStatus(int idx)
            {
                _haveItems[idx].IsEquip = !_haveItems[idx].IsEquip;

                if (_haveItems[idx].ItemType == 1)
                {
                    if (_hands.Count == 0)
                    {
                        _hands.Add(_haveItems[idx]);
                    }
                    else
                    {
                        _hands[0].IsEquip = !_hands[0].IsEquip;
                        _hands.Clear();
                        _hands.Add(_haveItems[idx]);
                    }
                }
                if (_haveItems[idx].ItemType == 2)
                {
                    if (_body.Count == 0)
                    {
                        _body.Add(_haveItems[idx]);
                    }
                    else
                    {
                        _body[0].IsEquip = !_body[0].IsEquip;
                        _body.Clear();
                        _body.Add(_haveItems[idx]);
                    }
                }
                if (_haveItems[idx].ItemType == 3)
                {
                    if (_boots.Count == 0)
                    {
                        _boots.Add(_haveItems[idx]);
                    }
                    else
                    {
                        _boots[0].IsEquip = !_boots[0].IsEquip;
                        _boots.Clear();
                        _boots.Add(_haveItems[idx]);
                    }
                }
                if (_haveItems[idx].ItemType == 4)
                {
                    if (_trinket.Count == 0)
                    {
                        _trinket.Add(_haveItems[idx]);
                    }
                    else
                    {
                        _trinket[0].IsEquip = !_trinket[0].IsEquip;
                        _trinket.Clear();
                        _trinket.Add(_haveItems[idx]);
                    }
                }
                if (_haveItems[idx].ItemType == 5)
                {
                    if (_head.Count == 0)
                    {
                        _head.Add(_haveItems[idx]);
                    }
                    else
                    {
                        _head[0].IsEquip = !_head[0].IsEquip;
                        _head.Clear();
                        _head.Add(_haveItems[idx]);
                    }
                }

            }

            private static void Shop()
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("1.구매하기");
                Console.WriteLine("2.판매하기");
                Console.WriteLine("0.뒤로가기");

                switch (CheckInput(0, 2))
                {
                    case 0:
                        Start();
                        break;
                    case 1:
                        Buy();
                        break;
                    case 2:
                        Sell();
                        break;
                }
            }

            private static void Buy()
            {

                Console.Clear();
                Console.WriteLine("무엇이 필요하신가요?");
                Console.WriteLine("소지 골드 : " + _player.Gold + "G");
                Console.WriteLine();
                for (int i = 0; i < Items.ShopItemCnt; i++)
                {
                    _items[i].ItemShopAbout(true, i + 1);
                }
                Console.WriteLine();
                Console.WriteLine("0.뒤로가기");
                int keyInput = CheckInput(0, Items.ShopItemCnt);
                switch (keyInput)
                {
                    case 0:
                        Shop();
                        break;
                    default:
                        toggleItemBuy(keyInput - 1);
                        Buy();
                        break;
                }

            }

            private static void toggleItemBuy(int idx)
            {
                if (_player.Gold >= _items[idx].Price && _items[idx].IsOnSale)
                {
                    _items[idx].AlreadyHave = true;
                    AddItem(item: _items[idx]);
                    _player.Gold -= _items[idx].Price;
                    _items[idx].IsOnSale = false;
                }
                else if (_player.Gold < _items[idx].Price)
                {
                    Console.WriteLine("골드가 부족합니다.");
                    Console.ReadKey();
                }
                else if (!_items[idx].IsOnSale)
                {
                    Console.WriteLine("이미 구매하셨습니다.");
                    Console.ReadKey();
                }
            }

            private static void Sell()
            {
                Console.Clear();
                Console.WriteLine("무엇을 판매하실건가요?");
                Console.WriteLine("소지 골드 : " + _player.Gold + "G");
                Console.WriteLine("");
                for (int i = 0; i < Items.itemCnt; i++)
                {
                    _haveItems[i].ItemSellAbout(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0.뒤로가기");
                int keyInput = CheckInput(0, Items.itemCnt);
                switch (keyInput)
                {
                    case 0:
                        Shop();
                        break;
                    default:
                        toggleItemSell(keyInput - 1);
                        Sell();
                        break;
                }


            }
            private static void toggleItemSell(int idx)
            {
                if (_haveItems[idx].IsEquip)
                {
                    _haveItems[idx].IsEquip = false;
                }
                _haveItems[idx].AlreadyHave = false;
                RemoveItem(item: _haveItems[idx]);
                _player.Gold += _haveItems[idx].ResellPrice;
                _haveItems[idx].IsOnSale = true;

            }


            private static void PrintStartLogo()
            {
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("                 스파르타 던전                  ");
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("");
                Console.WriteLine("               PRESS ANYKEY TO START            ");
                Console.ReadKey();
            }

        }



    }
}
