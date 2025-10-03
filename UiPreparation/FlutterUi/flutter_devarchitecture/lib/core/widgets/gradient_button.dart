import 'package:flutter/material.dart';

Widget gradientButton({
  required VoidCallback onPressed,
  required String text,
}) {
  return Container(
    decoration: BoxDecoration(
      gradient: const LinearGradient(
        // colors: [Color(0xFFFFAA4A), Color(0xFFF8405F)],
        colors: [Colors.black87, Colors.black87],
        begin: Alignment.topLeft,
        end: Alignment.bottomRight,
      ),
      borderRadius: BorderRadius.circular(8),
    ),
    child: ElevatedButton(
      onPressed: onPressed,
      style: ElevatedButton.styleFrom(
        minimumSize: const Size(double.infinity, 60),
        padding: const EdgeInsets.symmetric(vertical: 12),
        backgroundColor:
            Colors.transparent, // Gradient kullanıldığı için şeffaf
        shadowColor: Colors.transparent,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Text(
        text,
        style:
            const TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
    ),
  );
}
