// === main.ink ===
// 1. 匯入所有的變數與劇情檔案
INCLUDE global.ink
INCLUDE day_01.ink
INCLUDE day_02.ink
INCLUDE day_03.ink

// 2. 遊戲的真正起始點
-> game_start

=== game_start ===
【系統測試：多檔案協作架構已啟動】
遊戲即將開始...
-> day01_start // 交接給第一天檔案

// 3. 中轉站：處理第一天結束後的邏輯
=== main_flow_check_day1 ===
系統提示：第一天結束了，即將進入下一天...
-> day02_start // 交接給第二天檔案

// 4. 中轉站：處理第二天結束後的邏輯
=== main_flow_check_day2 ===
系統提示：第二天結束了，即將迎來最終結局...
-> day03_start // 交接給第三天檔案