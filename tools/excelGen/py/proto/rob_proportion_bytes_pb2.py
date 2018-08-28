# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/rob_proportion_bytes.proto

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
  name='proto/rob_proportion_bytes.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n proto/rob_proportion_bytes.proto\x12 com.game.framework.resource.data\"\x91\x01\n\x0eROB_PROPORTION\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x18\n\rtransit_depot\x18\x02 \x01(\x05:\x01\x30\x12\x18\n\rbig_warehouse\x18\x03 \x01(\x05:\x01\x30\x12\x11\n\x06\x62w_lim\x18\x04 \x01(\x05:\x01\x30\x12\x14\n\tgold_prop\x18\x05 \x01(\x05:\x01\x30\x12\x13\n\x08gold_lim\x18\x06 \x01(\x05:\x01\x30\"W\n\x14ROB_PROPORTION_ARRAY\x12?\n\x05items\x18\x01 \x03(\x0b\x32\x30.com.game.framework.resource.data.ROB_PROPORTION')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_ROB_PROPORTION = _descriptor.Descriptor(
  name='ROB_PROPORTION',
  full_name='com.game.framework.resource.data.ROB_PROPORTION',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.ROB_PROPORTION.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='transit_depot', full_name='com.game.framework.resource.data.ROB_PROPORTION.transit_depot', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='big_warehouse', full_name='com.game.framework.resource.data.ROB_PROPORTION.big_warehouse', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='bw_lim', full_name='com.game.framework.resource.data.ROB_PROPORTION.bw_lim', index=3,
      number=4, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='gold_prop', full_name='com.game.framework.resource.data.ROB_PROPORTION.gold_prop', index=4,
      number=5, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='gold_lim', full_name='com.game.framework.resource.data.ROB_PROPORTION.gold_lim', index=5,
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
  serialized_start=71,
  serialized_end=216,
)


_ROB_PROPORTION_ARRAY = _descriptor.Descriptor(
  name='ROB_PROPORTION_ARRAY',
  full_name='com.game.framework.resource.data.ROB_PROPORTION_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.ROB_PROPORTION_ARRAY.items', index=0,
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
  serialized_start=218,
  serialized_end=305,
)

_ROB_PROPORTION_ARRAY.fields_by_name['items'].message_type = _ROB_PROPORTION
DESCRIPTOR.message_types_by_name['ROB_PROPORTION'] = _ROB_PROPORTION
DESCRIPTOR.message_types_by_name['ROB_PROPORTION_ARRAY'] = _ROB_PROPORTION_ARRAY

ROB_PROPORTION = _reflection.GeneratedProtocolMessageType('ROB_PROPORTION', (_message.Message,), dict(
  DESCRIPTOR = _ROB_PROPORTION,
  __module__ = 'proto.rob_proportion_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ROB_PROPORTION)
  ))
_sym_db.RegisterMessage(ROB_PROPORTION)

ROB_PROPORTION_ARRAY = _reflection.GeneratedProtocolMessageType('ROB_PROPORTION_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _ROB_PROPORTION_ARRAY,
  __module__ = 'proto.rob_proportion_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ROB_PROPORTION_ARRAY)
  ))
_sym_db.RegisterMessage(ROB_PROPORTION_ARRAY)


# @@protoc_insertion_point(module_scope)
