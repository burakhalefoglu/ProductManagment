import '../../../../models/i_dto.dart';

class TranslateDto implements IDto {
  int id;
  String code;
  String language;
  int languageId;
  String value;

  TranslateDto(
      {required this.id,
      required this.code,
      required this.language,
      required this.languageId,
      required this.value});

  factory TranslateDto.fromMap(Map<String, dynamic> map) {
    return TranslateDto(
      id: int.parse(map['id'].toString()),
      code: map['code'] ?? "",
      language: map['language'] ?? "",
      value: map['value'] ?? "",
      languageId: int.parse(map['languageId'].toString()),
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'code': code,
      'language': language,
      'value': value,
      'languageId': languageId
    };
  }
}
