/**
 * jQuery SideBar 1.4.3
 * 
 * Copyright (c) 2009-2015 www.bzway.com. All rights reserved.
 *
 * Licensed under the GPL license: http://www.gnu.org/licenses/gpl.txt
 * To use it on other terms please contact us at zhumingwu@bzway.com
 *
 */

(function ($) {
     // �����ǲ�������ڣ�����һ��˽�з���
     var privateFunction = function () {
         // code here
     };

     // ͨ������������һ�����󣬴洢������Ҫ�Ĺ��з���
     var methods = {
         // �������������ж���ÿ�������ķ���
         init: function () {
             // Ϊ�˸��õ�����ԣ���������������������ÿ�������е�ѡ�������е�ÿ��������Ԫ�ض�ִ�д���
             return this.each(function () {
                 // Ϊÿ��������Ԫ�ش���һ��jQuery����
                 var $this = $(this);
                 // ִ�д��� ���磺privateFunction()
             });
         },
         destroy: function () {
             // ��ѡ����ÿ��Ԫ�ض�ִ�з���
             return this.each(function () {
                 // ִ�д���
             });
         }
     };

     $.fn.SideBar = function () {
         var $this = $(this);
         var div = $this.children("div");
         ui = $this.width();
         $(div[0]).addClass("float-left");
         $(div[1]).addClass("float-right");

         //var width = Math.round($(div[0]).width() / $this.width() * 100);
         //if (width == 100) {
         //    width = 20;
         //}
         //$(div[0]).css("width", width + "%");
         //width = Math.round($(div[1]).size.width() / $this.width() * 100);
         //$(div[1]).css("width", width + "%");
     };

 })(jQuery);