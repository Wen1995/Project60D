# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/item_res_data.proto

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
  name='proto/item_res_data.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x19proto/item_res_data.proto\x12 com.game.framework.resource.data\"\xf8\x02\n\x08ITEM_RES\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x13\n\x08item_lvl\x18\x02 \x01(\x05:\x01\x30\x12\x12\n\x08min_name\x18\x03 \x01(\t:\x00\x12\x13\n\ticon_name\x18\x04 \x01(\t:\x00\x12\x19\n\x0fsmall_icon_name\x18\x05 \x01(\t:\x00\x12\x0e\n\x04\x64\x65sc\x18\x06 \x01(\t:\x00\x12\x13\n\tgold_conv\x18\x07 \x01(\t:\x00\x12\x15\n\nmultiplier\x18\x08 \x01(\x05:\x01\x30\x12\x15\n\ntime_cost1\x18\t \x01(\x05:\x01\x30\x12\x14\n\tele_cost1\x18\n \x01(\x05:\x01\x30\x12\x14\n\tcost_qty1\x18\x0b \x01(\x05:\x01\x30\x12\x15\n\nresult_id1\x18\x0c \x01(\x05:\x01\x30\x12\x15\n\ntime_cost2\x18\r \x01(\x05:\x01\x30\x12\x14\n\tele_cost2\x18\x0e \x01(\x05:\x01\x30\x12\x14\n\tcost_qty2\x18\x0f \x01(\x05:\x01\x30\x12\x15\n\nresult_id2\x18\x10 \x01(\x05:\x01\x30\x12\x14\n\tstor_unit\x18\x11 \x01(\x05:\x01\x30\"K\n\x0eITEM_RES_ARRAY\x12\x39\n\x05items\x18\x01 \x03(\x0b\x32*.com.game.framework.resource.data.ITEM_RES')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_ITEM_RES = _descriptor.Descriptor(
  name='ITEM_RES',
  full_name='com.game.framework.resource.data.ITEM_RES',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.ITEM_RES.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='item_lvl', full_name='com.game.framework.resource.data.ITEM_RES.item_lvl', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='min_name', full_name='com.game.framework.resource.data.ITEM_RES.min_name', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='icon_name', full_name='com.game.framework.resource.data.ITEM_RES.icon_name', index=3,
      number=4, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='small_icon_name', full_name='com.game.framework.resource.data.ITEM_RES.small_icon_name', index=4,
      number=5, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='desc', full_name='com.game.framework.resource.data.ITEM_RES.desc', index=5,
      number=6, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='gold_conv', full_name='com.game.framework.resource.data.ITEM_RES.gold_conv', index=6,
      number=7, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='multiplier', full_name='com.game.framework.resource.data.ITEM_RES.multiplier', index=7,
      number=8, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='time_cost1', full_name='com.game.framework.resource.data.ITEM_RES.time_cost1', index=8,
      number=9, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='ele_cost1', full_name='com.game.framework.resource.data.ITEM_RES.ele_cost1', index=9,
      number=10, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cost_qty1', full_name='com.game.framework.resource.data.ITEM_RES.cost_qty1', index=10,
      number=11, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='result_id1', full_name='com.game.framework.resource.data.ITEM_RES.result_id1', index=11,
      number=12, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='time_cost2', full_name='com.game.framework.resource.data.ITEM_RES.time_cost2', index=12,
      number=13, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='ele_cost2', full_name='com.game.framework.resource.data.ITEM_RES.ele_cost2', index=13,
      number=14, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cost_qty2', full_name='com.game.framework.resource.data.ITEM_RES.cost_qty2', index=14,
      number=15, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='result_id2', full_name='com.game.framework.resource.data.ITEM_RES.result_id2', index=15,
      number=16, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='stor_unit', full_name='com.game.framework.resource.data.ITEM_RES.stor_unit', index=16,
      number=17, type=5, cpp_type=1, label=1,
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
  serialized_start=64,
  serialized_end=440,
)


_ITEM_RES_ARRAY = _descriptor.Descriptor(
  name='ITEM_RES_ARRAY',
  full_name='com.game.framework.resource.data.ITEM_RES_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.ITEM_RES_ARRAY.items', index=0,
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
  serialized_start=442,
  serialized_end=517,
)

_ITEM_RES_ARRAY.fields_by_name['items'].message_type = _ITEM_RES
DESCRIPTOR.message_types_by_name['ITEM_RES'] = _ITEM_RES
DESCRIPTOR.message_types_by_name['ITEM_RES_ARRAY'] = _ITEM_RES_ARRAY

ITEM_RES = _reflection.GeneratedProtocolMessageType('ITEM_RES', (_message.Message,), dict(
  DESCRIPTOR = _ITEM_RES,
  __module__ = 'proto.item_res_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ITEM_RES)
  ))
_sym_db.RegisterMessage(ITEM_RES)

ITEM_RES_ARRAY = _reflection.GeneratedProtocolMessageType('ITEM_RES_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _ITEM_RES_ARRAY,
  __module__ = 'proto.item_res_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ITEM_RES_ARRAY)
  ))
_sym_db.RegisterMessage(ITEM_RES_ARRAY)


# @@protoc_insertion_point(module_scope)
