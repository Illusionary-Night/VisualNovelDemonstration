// 1. 遊戲一開始，先宣告會用到的變數
VAR affection = 0       // 對象的好感度
VAR has_coffee = false  // 有沒有買咖啡（false代表沒有，true代表有）

// 2. 遊戲起點，直接跳轉到第一幕
-> scene_01_street

// 3. 定義第一幕：街道上
=== scene_01_street ===
今天是個大晴天。你走在前往社辦的路上。
路過便利商店時，你想著要不要買點什麼。

// 玩家的選項：
* [買一杯熱咖啡 (花費 50 元)]
    // 執行數值變動
    ~ has_coffee = true
    你走進超商，買了一杯熱騰騰的拿鐵。
    -> scene_02_clubroom // 選擇後跳轉到社辦場景

* [省錢，直接去社辦]
    你摸了摸乾癟的錢包，決定快步前往社辦。
    -> scene_02_clubroom // 選擇後跳轉到社辦場景

// 4. 定義第二幕：社辦
=== scene_02_clubroom ===
你推開社辦的門，看到美術總監「小亞」正趴在桌上，看起來很累的樣子。
小亞：「早安...我熬夜趕圖，現在好想睡覺...」

* [關心她]
    ~ affection = affection + 10 // 好感度 + 10
    你拍了拍她的肩膀：「辛苦了，專案進度還好嗎？」
    小亞微微一笑：「有你這句話就好多了。」
    -> check_ending

* {has_coffee == true} [把熱咖啡遞給她] 
    // 上面這個選項，只有在剛剛有買咖啡的情況下才會出現！
    ~ affection = affection + 50 // 好感度大飆升
    你把剛買的熱拿鐵放在她桌上。
    小亞眼睛一亮：「天啊！你怎麼知道我需要這個！你最棒了！」
    -> check_ending

* [催促進度]
    ~ affection = affection - 20 // 好感度扣 20
    你皺起眉頭：「立繪什麼時候可以交？」
    小亞瞪了你一眼，默默把頭轉過去不理你。
    -> check_ending

// 5. 定義結局結算判定
=== check_ending ===
// 系統根據好感度，自動決定結局走向
{ 
  - affection >= 50:
    -> ending_good
  - affection < 0:
    -> ending_bad
  - else:
    -> ending_normal
}

// 6. 各種結局的分支
=== ending_good ===
【True End】小亞充滿幹勁，視覺小說順利完成，大受好評！
-> END

=== ending_normal ===
【Normal End】遊戲勉強壓線做完了，但大家都覺得有點肝。
-> END

=== ending_bad ===
【Bad End】團隊氣氛降至冰點，專案無限期延宕...
-> END