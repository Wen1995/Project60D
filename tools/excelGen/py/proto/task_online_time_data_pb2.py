# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/task_online_time_data.proto

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
  name='proto/task_online_time_data.proto',
  package='com.game.server.data',
  serialized_pb=_b('\n!proto/task_online_time_data.proto\x12\x14\x63om.game.server.data\"x\n\x10TASK_ONLINE_TIME\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x12\n\x07\x63\x64_time\x18\x02 \x01(\x05:\x01\x30\x12\x14\n\tqty_limit\x18\x03 \x01(\x05:\x01\x30\x12\x14\n\tstar_dust\x18\x04 \x01(\x05:\x01\x30\x12\x15\n\ntech_point\x18\x05 \x01(\x05:\x01\x30\"O\n\x16TASK_ONLINE_TIME_ARRAY\x12\x35\n\x05items\x18\x01 \x03(\x0b\x32&.com.game.server.data.TASK_ONLINE_TIME')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_TASK_ONLINE_TIME = _descriptor.Descriptor(
  name='TASK_ONLINE_TIME',
  full_name='com.game.server.data.TASK_ONLINE_TIME',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.server.data.TASK_ONLINE_TIME.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cd_time', full_name='com.game.server.data.TASK_ONLINE_TIME.cd_time', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='qty_limit', full_name='com.game.server.data.TASK_ONLINE_TIME.qty_limit', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='star_dust', full_name='com.game.server.data.TASK_ONLINE_TIME.star_dust', index=3,
      number=4, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='tech_point', full_name='com.game.server.data.TASK_ONLINE_TIME.tech_point', index=4,
      number=5, type=5, cpp_type=1, label=1,
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
  serialized_start=59,
  serialized_end=179,
)


_TASK_ONLINE_TIME_ARRAY = _descriptor.Descriptor(
  name='TASK_ONLINE_TIME_ARRAY',
  full_name='com.game.server.data.TASK_ONLINE_TIME_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.server.data.TASK_ONLINE_TIME_ARRAY.items', index=0,
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
  serialized_start=181,
  serialized_end=260,
)

_TASK_ONLINE_TIME_ARRAY.fields_by_name['items'].message_type = _TASK_ONLINE_TIME
DESCRIPTOR.message_types_by_name['TASK_ONLINE_TIME'] = _TASK_ONLINE_TIME
DESCRIPTOR.message_types_by_name['TASK_ONLINE_TIME_ARRAY'] = _TASK_ONLINE_TIME_ARRAY

TASK_ONLINE_TIME = _reflection.GeneratedProtocolMessageType('TASK_ONLINE_TIME', (_message.Message,), dict(
  DESCRIPTOR = _TASK_ONLINE_TIME,
  __module__ = 'proto.task_online_time_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.server.data.TASK_ONLINE_TIME)
  ))
_sym_db.RegisterMessage(TASK_ONLINE_TIME)

TASK_ONLINE_TIME_ARRAY = _reflection.GeneratedProtocolMessageType('TASK_ONLINE_TIME_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _TASK_ONLINE_TIME_ARRAY,
  __module__ = 'proto.task_online_time_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.server.data.TASK_ONLINE_TIME_ARRAY)
  ))
_sym_db.RegisterMessage(TASK_ONLINE_TIME_ARRAY)


# @@protoc_insertion_point(module_scope)
