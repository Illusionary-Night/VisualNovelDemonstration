// === day_01.ink ===
// 注意：節點名稱以 day01_ 開頭，避免與其他人撞名

=== day01_start ===
【第一天：相遇】
今天是開學的第一天，你帶著 {money} 元出門。
# play_sound: dingding
在去學校的路上，你看到美術總監小亞正在咖啡廳前看著菜單猶豫。

* [花 50 元幫她買杯咖啡]
    ~ money = money - 50
    ~ has_coffee = true
    ~ affection_A = affection_A + 10
    你大方地請了她一杯熱拿鐵，她對你露出燦爛的笑容。
    -> day01_end
    
* [跟她打聲招呼就走]
    ~ affection_A = affection_A + 5
    你笑著向她揮揮手，然後趕去教室。
    -> day01_end

=== day01_end ===
第一天的行程就這樣平淡地結束了。
-> main_flow_check_day1 // 將主導權還給 main.ink