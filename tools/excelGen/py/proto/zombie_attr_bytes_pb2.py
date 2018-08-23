# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/zombie_attr_bytes.proto

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
  name='proto/zombie_attr_bytes.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x1dproto/zombie_attr_bytes.proto\x12 com.game.framework.resource.data\"\x93\x01\n\x0bZOMBIE_ATTR\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x1a\n\x0fmanorcap_zombie\x18\x02 \x01(\x05:\x01\x30\x12\x15\n\nzombie_atk\x18\x03 \x01(\x05:\x01\x30\x12\x15\n\nzombie_def\x18\x04 \x01(\x05:\x01\x30\x12\x14\n\tzombie_hp\x18\x05 \x01(\x05:\x01\x30\x12\x15\n\nzombie_num\x18\x06 \x01(\x05:\x01\x30\"Q\n\x11ZOMBIE_ATTR_ARRAY\x12<\n\x05items\x18\x01 \x03(\x0b\x32-.com.game.framework.resource.data.ZOMBIE_ATTR')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_ZOMBIE_ATTR = _descriptor.Descriptor(
  name='ZOMBIE_ATTR',
  full_name='com.game.framework.resource.data.ZOMBIE_ATTR',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='manorcap_zombie', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.manorcap_zombie', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='zombie_atk', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.zombie_atk', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='zombie_def', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.zombie_def', index=3,
      number=4, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='zombie_hp', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.zombie_hp', index=4,
      number=5, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='zombie_num', full_name='com.game.framework.resource.data.ZOMBIE_ATTR.zombie_num', index=5,
      number=6, type=5, cpp_type=1, label=1,
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
  serialized_end=215,
)


_ZOMBIE_ATTR_ARRAY = _descriptor.Descriptor(
  name='ZOMBIE_ATTR_ARRAY',
  full_name='com.game.framework.resource.data.ZOMBIE_ATTR_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.ZOMBIE_ATTR_ARRAY.items', index=0,
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
  serialized_start=217,
  serialized_end=298,
)

_ZOMBIE_ATTR_ARRAY.fields_by_name['items'].message_type = _ZOMBIE_ATTR
DESCRIPTOR.message_types_by_name['ZOMBIE_ATTR'] = _ZOMBIE_ATTR
DESCRIPTOR.message_types_by_name['ZOMBIE_ATTR_ARRAY'] = _ZOMBIE_ATTR_ARRAY

ZOMBIE_ATTR = _reflection.GeneratedProtocolMessageType('ZOMBIE_ATTR', (_message.Message,), dict(
  DESCRIPTOR = _ZOMBIE_ATTR,
  __module__ = 'proto.zombie_attr_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ZOMBIE_ATTR)
  ))
_sym_db.RegisterMessage(ZOMBIE_ATTR)

ZOMBIE_ATTR_ARRAY = _reflection.GeneratedProtocolMessageType('ZOMBIE_ATTR_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _ZOMBIE_ATTR_ARRAY,
  __module__ = 'proto.zombie_attr_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ZOMBIE_ATTR_ARRAY)
  ))
_sym_db.RegisterMessage(ZOMBIE_ATTR_ARRAY)


# @@protoc_insertion_point(module_scope)
