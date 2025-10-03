import 'package:flutter/material.dart';
import '../../routes/routes_constants.dart';
import 'helper.dart';
import '../../core/constants/sidebar_constants.dart';

class NavBar extends StatefulWidget {
  const NavBar({super.key});

  @override
  State<NavBar> createState() => _NavBarState();
}

class _NavBarState extends State<NavBar> {
  final ValueNotifier<Widget?> adminPanelMenu = ValueNotifier<Widget?>(null);
  final ValueNotifier<Widget?> appPanelMenu = ValueNotifier<Widget?>(null);

  bool _menuItemsLoaded = false; // Tekrar yüklemeyi önlemek için flag.

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();

    if (!_menuItemsLoaded) {
      _menuItemsLoaded = true;
      _loadMenuItems();
    }
  }

  Future<void> _loadMenuItems() async {
    adminPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.apps,
      SidebarConstants.adminPanelPageTitle,
      [
        {
          "name": SidebarConstants.adminPanelHomePageTitle,
          "route": RoutesConstants.adminHomePage,
          "icon": Icons.home_work_rounded,
          "guard": 'GetUsersQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageUserTitle,
          "icon": Icons.person,
          "guard": 'GetUsersQuery',
          "subMenu": [
            {
              "name": SidebarConstants.adminPanelPageUserTitle,
              "route": RoutesConstants.adminUserPage,
              "icon": Icons.person,
              "guard": 'GetUsersQuery',
            },
          ],
        },
        {
          "name": SidebarConstants.adminPanelPageGroupTitle,
          "route": RoutesConstants.adminGroupPage,
          "icon": Icons.groups,
          "guard": 'GetGroupsQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageOperationClaimTitle,
          "route": RoutesConstants.adminOperationClaimPage,
          "icon": Icons.security,
          "guard": 'GetOperationClaimsQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageLanguageTitle,
          "route": RoutesConstants.adminLanguagePage,
          "icon": Icons.language,
          "guard": 'GetLanguagesQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageTranslateTitle,
          "route": RoutesConstants.adminTranslatePage,
          "icon": Icons.translate,
          "guard": 'GetTranslatesQuery',
        },
        {
          "name": SidebarConstants.adminPanelPageLogTitle,
          "route": RoutesConstants.adminLogPage,
          "icon": Icons.history,
          "guard": 'GetLogDtoQuery',
        },
      ],
    );

    appPanelMenu.value = await buildNavWithSubMenuItemElement(
      context,
      Icons.format_list_bulleted_sharp,
      SidebarConstants.appPanelPageTitle,
      [
        {
          "name": SidebarConstants.homePageTitle,
          "route": RoutesConstants.appHomePage,
          "icon": Icons.home,
        },
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        children: [
          buildLogoWidget(),
          buildNavElement(
            Icons.home_outlined,
            SidebarConstants.homePageTitle,
            RoutesConstants.appHomePage,
          ),
          // Admin Panel
          ValueListenableBuilder<Widget?>(
            valueListenable: adminPanelMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const SizedBox.shrink();
              }
              return menu;
            },
          ),
          // App Panel
          ValueListenableBuilder<Widget?>(
            valueListenable: appPanelMenu,
            builder: (context, menu, _) {
              if (menu == null) {
                return const SizedBox.shrink();
              }
              return menu;
            },
          ),
        ],
      ),
    );
  }
}
