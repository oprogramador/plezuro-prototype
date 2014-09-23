#
# db.rb
# Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
# 
# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 2 of the License, or
# (at your option) any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
# MA 02110-1301, USA.
# 
# 
#
 

require 'mysql'
db = Mysql.new(ARGV[0], ARGV[1], ARGV[2], ARGV[3])
#db.options(Mysql::SET_CHARSET_NAME, 'unicode')
res = db.query('show tables')
tables = []
res.each do |i|
	tables << i[0]
end
tables.each do |table|
	columns = []
	res = db.query('show columns from '+table)
	res.each do |i|
		columns << i[0]
	end
	res = db.query('select * from '+table)
	res.each do |row|
		data = []
		data << 'table' << table
		row.each_with_index do |val,i|
			data << columns[i] << val
		end
		print data.join("\t"),"\n"
	end
end
