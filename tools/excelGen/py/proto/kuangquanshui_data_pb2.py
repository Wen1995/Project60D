# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/kuangquanshui_data.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='proto/kuangquanshui_data.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x1eproto/kuangquanshui_data.proto\x12 com.game.framework.resource.data\"Z\n\rKUANGQUANSHUI\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x1c\n\x11kuangquanshui_spd\x18\x02 \x01(\x05:\x01\x30\x12\x1c\n\x11kuangquanshui_cap\x18\x03 \x01(\x05:\x01\x30\"U\n\x13KUANGQUANSHUI_ARRAY\x12>\n\x05items\x18\x01 \x03(\x0b\x32/.com.game.framework.resource.data.KUANGQUANSHUI')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_KUANGQUANSHUI = _descriptor.Descriptor(
  name='KUANGQUANSHUI',
  full_name='com.game.framework.resource.data.KUANGQUANSHUI',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.KUANGQUANSHUI.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='kuangquanshui_spd', full_name='com.game.framework.resource.data.KUANGQUANSHUI.kuangquanshui_spd', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='kuangquanshui_cap', full_name='com.game.framework.resource.data.KUANGQUANSHUI.kuangquanshui_cap', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=68,
  serialized_end=158,
)


_KUANGQUANSHUI_ARRAY = _descriptor.Descriptor(
  name='KUANGQUANSHUI_ARRAY',
  full_name='com.game.framework.resource.data.KUANGQUANSHUI_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.KUANGQUANSHUI_ARRAY.items', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=160,
  serialized_end=245,
)

_KUANGQUANSHUI_ARRAY.fields_by_name['items'].message_type = _KUANGQUANSHUI
DESCRIPTOR.message_types_by_name['KUANGQUANSHUI'] = _KUANGQUANSHUI
DESCRIPTOR.message_types_by_name['KUANGQUANSHUI_ARRAY'] = _KUANGQUANSHUI_ARRAY

KUANGQUANSHUI = _reflection.GeneratedProtocolMessageType('KUANGQUANSHUI', (_message.Message,), dict(
  DESCRIPTOR = _KUANGQUANSHUI,
  __module__ = 'proto.kuangquanshui_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.KUANGQUANSHUI)
  ))
_sym_db.RegisterMessage(KUANGQUANSHUI)

KUANGQUANSHUI_ARRAY = _reflection.GeneratedProtocolMessageType('KUANGQUANSHUI_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _KUANGQUANSHUI_ARRAY,
  __module__ = 'proto.kuangquanshui_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.KUANGQUANSHUI_ARRAY)
  ))
_sym_db.RegisterMessage(KUANGQUANSHUI_ARRAY)


# @@protoc_insertion_point(module_scope)