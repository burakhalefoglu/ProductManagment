class FileContent {
  final String title;
  final String base64String;
  final String type;

  FileContent(
      {required this.title, required this.base64String, required this.type});

  factory FileContent.fromMap(Map<String, dynamic> Map) => FileContent(
        title: Map['title'] as String? ?? '',
        base64String: Map['base64String'] as String? ?? '',
        type: Map['type'] as String? ?? '',
      );

  Map<String, dynamic> toMap() => {
        'title': title,
        'base64String': base64String,
        'type': type,
      };
}
