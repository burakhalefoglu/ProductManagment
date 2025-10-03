import '/core/di/core_initializer.dart';
import '/core/utilities/results.dart';

import '../../../../services/base_services/api_service.dart';
import '../models/user.dart';
import 'i_user_service.dart';

class ApiUserService extends ApiService<User> implements IUserService {
  ApiUserService({required super.method});
  Future<IDataResult<Map<String, dynamic>>> getUserByUserIdWithToken() async {
    var result = await CoreInitializer()
        .coreContainer
        .http
        .get(url + "/get-user-by-user-id");
    if (result["success"] != null) {
      if (result["success"] == false) {
        return Future.value(FailureDataResult(result["message"] ?? ""));
      }
    }
    return Future.value(SuccessDataResult(result, ""));
  }
}
