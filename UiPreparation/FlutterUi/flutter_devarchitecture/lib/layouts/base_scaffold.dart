import 'dart:ui';
import 'package:compare_it/core/theme/extensions.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:provider/provider.dart';
import '../core/constants/core_screen_texts.dart';
import '../core/theme/custom_colors.dart';
import '../core/di/core_initializer.dart';
import '../core/theme/theme_provider.dart';
import '../routes/routes_constants.dart';
import 'sidebar/sidebar.dart';

buildBaseScaffold(BuildContext context, Widget body, {bool isDrawer = true}) {
  final themeProvider = Provider.of<ThemeProvider>(context);
  final isDark = ThemeProvider.isDarkMode();

  return Scaffold(
    extendBodyBehindAppBar: true,
    appBar: kIsWeb ? null : AppBar(
      elevation: 0,
      backgroundColor: Colors.transparent,
      title: context.isMobile ? const SizedBox() : const Text(
        "COMPARE IT",
        style: TextStyle(fontWeight: FontWeight.w800, fontSize: 26, fontFamily: 'Inter'),
      ),
      flexibleSpace: ClipRect(
        child: BackdropFilter(
          filter: ImageFilter.blur(sigmaX: 20, sigmaY: 20),
          child: Container(
            decoration: BoxDecoration(
              color: isDark ? Colors.white.withOpacity(0.06) : Colors.white.withOpacity(0.10),
              border: Border(
                bottom: BorderSide(
                  color: (isDark ? Colors.white : Colors.black).withOpacity(0.08),
                  width: 1,
                ),
              ),
            ),
          ),
        ),
      ),
      actions: ModalRoute.of(context)?.settings.name == RoutesConstants.loginPage || !isDrawer
          ? [buildThemeButton(context, themeProvider)]
          : [
        buildThemeButton(context, themeProvider),
        buildNotificationButton(context),
        buildProfileButton(context),
        buildLogOutButton(context),
      ],
      leading: Builder(
        builder: (context) => isDrawer
            ? IconButton(
          onPressed: () => Scaffold.of(context).openDrawer(),
          icon: const Icon(Icons.menu),
        )
            : const SizedBox(),
      ),
    ),
    drawer: isDrawer ? const NavBar() : null,

    // 🔽 Arkaya doku eklendi
    body: Stack(
      children: [
        // 1) Arkaplan (gradient veya image)
        Positioned.fill(
          child: DecoratedBox(
            decoration: BoxDecoration(
              gradient: isDark
                  ? const LinearGradient(
                begin: Alignment.topLeft,
                end: Alignment.bottomRight,
                colors: [Color(0xFF0B1220), Color(0xFF111827)],
              )
                  : const LinearGradient(
                begin: Alignment.topLeft,
                end: Alignment.bottomRight,
                colors: [Color(0xFFEAF0F7), Color(0xFFF8FAFC)],
              ),
            ),
          ),
        ),

        // 2) Sayfanın asıl içeriği
        Positioned.fill(child: body),
      ],
    ),
  );
}

Widget buildThemeButton(BuildContext context, ThemeProvider themeProvider) {
  return Tooltip(
    message: CoreScreenTexts.changeThemeButton,
    child: IconButton(
      icon: Icon(
        themeProvider.themeMode == ThemeMode.dark
            ? Icons.dark_mode
            : Icons.light_mode,
      ),
      onPressed: () {
        themeProvider.toggleTheme();
      },
    ),
  );
}

Widget buildNotificationButton(BuildContext context) {
  var hasNotification = false;
  return Tooltip(
    message: CoreScreenTexts.notificationsButton,
    child: Stack(
      children: [
        IconButton(
          onPressed: () {
            CoreInitializer()
                .coreContainer
                .screenMessage
                .getInfoMessage("Hiç yeni bildirim yok");

            // 3 sn sonra kapanacak
            Future.delayed(const Duration(seconds: 3), () {
              if (Navigator.canPop(context)) {
                Navigator.pop(context);
              }
            });
          },
          icon: const Icon(Icons.notifications_active_outlined),
        ),
        if (hasNotification)
          Positioned(
            top: 6,
            right: 6,
            child: Container(
              width: 12,
              height: 12,
              decoration: BoxDecoration(
                color: CustomColors.success.getColor,
                shape: BoxShape.circle,
              ),
            ),
          ),
      ],
    ),
  );
}

Widget buildProfileButton(BuildContext context) {
  return Tooltip(
    message: CoreScreenTexts.profileButton,
    child: IconButton(
      onPressed: () {
        Modular.to.navigate(RoutesConstants.profile);
      },
      icon: const Icon(Icons.account_circle_outlined),
    ),
  );
}

Widget buildLogOutButton(BuildContext context) {
  return Tooltip(
    message: CoreScreenTexts.logOutButton,
    child: IconButton(
      onPressed: () {
        CoreInitializer().coreContainer.storage.delete("userId");
        CoreInitializer().coreContainer.storage.delete("userName");
        CoreInitializer().coreContainer.storage.delete("token");
        Modular.to.navigate(RoutesConstants.loginPage);
      },
      icon: const Icon(Icons.logout),
    ),
  );
}
