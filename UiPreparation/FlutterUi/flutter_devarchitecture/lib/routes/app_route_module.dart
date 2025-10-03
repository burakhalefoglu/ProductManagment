import '/core/features/public/healthcheck/healthcheck_page.dart';
import '/features/home/home_page.dart';

import '../core/features/admin_panel/log_f/pages/log_page.dart';
import '../core/guard/claim_guard.dart';
import '../core/features/admin_panel/languages/pages/admin_language_page.dart';
import '../core/features/admin_panel/users/pages/user_page.dart';
import 'package:flutter_modular/flutter_modular.dart';

import '../core/guard/auth_guard.dart';
import '../core/features/admin_panel/groups/pages/admin_group_page.dart';
import '../core/features/admin_panel/operation_claims/pages/admin_operation_claim_page.dart';
import '../core/features/admin_panel/translates/pages/admin_translate_page.dart';
import '../features/auth/pages/login_page.dart';
import '../core/features/public/not_found/not_found_page.dart';
import 'routes_constants.dart';

class AppRouteModule extends Module {
  @override
  void binds(Injector i) {
    i.addSingleton<AuthStore>(AuthStore.new);
    super.binds(i);
  }

  @override
  void routes(r) {
    var transition = TransitionType.fadeIn;

    //*? APP HOME PAGE
    r.child(
      RoutesConstants.appHomePage,
      child: (context) => const HomePage(),
      transition: transition,
      guards: [ModularAuthGuard()],
    );

    // ADMIN LAYOUT
    //*? ADMIN HOME PAGE
    r.child(
      RoutesConstants.adminHomePage,
      child: (context) => const HomePage(),
      transition: transition,
      guards: [ModularAuthGuard(), ModularClaimGuard(claim: "GetLogDtoQuery")],
    );

    //*? ADMIN LOG PAGE
    r.child(
      RoutesConstants.adminLogPage,
      child: (context) => const AdminLogPage(),
      transition: transition,
      guards: [ModularAuthGuard(), ModularClaimGuard(claim: "GetLogDtoQuery")],
    );

    //*? ADMIN USER PAGE
    r.child(
      RoutesConstants.adminUserPage,
      child: (context) => const AdminUserPage(),
      transition: transition,
      guards: [ModularAuthGuard(), ModularClaimGuard(claim: "GetUsersQuery")],
    );

    //*? ADMIN GROUP PAGE
    r.child(
      RoutesConstants.adminGroupPage,
      child: (context) => const AdminGroupPage(),
      transition: transition,
      guards: [ModularAuthGuard(), ModularClaimGuard(claim: "GetGroupsQuery")],
    );

    //*? ADMIN OPERATION CLAIM PAGE
    r.child(
      RoutesConstants.adminOperationClaimPage,
      child: (context) => const AdminOperationClaimPage(),
      transition: transition,
      guards: [
        ModularAuthGuard(),
        ModularClaimGuard(claim: "GetOperationClaimsQuery"),
      ],
    );

    //*? ADMIN LANGUAGE PAGE
    r.child(
      RoutesConstants.adminLanguagePage,
      child: (context) => const AdminLanguagePage(),
      transition: transition,
      guards: [
        ModularAuthGuard(),
        ModularClaimGuard(claim: "GetLanguagesQuery"),
      ],
    );

    //*? ADMIN TRANSLATE PAGE
    r.child(
      RoutesConstants.adminTranslatePage,
      child: (context) => const AdminTranslatePage(),
      transition: transition,
      guards: [
        ModularAuthGuard(),
        ModularClaimGuard(claim: "GetTranslatesQuery"),
      ],
    );

    // PUBLIC LAYOUT
    //*? LOGIN PAGE
    r.child(
      RoutesConstants.loginPage,
      child: (context) => LoginPage(),
      transition: transition,
    );

    r.child(
      RoutesConstants.healthCheckPage,
      child: (context) => const HealthCheckPage(),
      transition: transition,
    );

    //*? WILDCARD
    r.wildcard(
      child: (context) => const NotFoundPage(),
      transition: transition,
    );
  }
}
