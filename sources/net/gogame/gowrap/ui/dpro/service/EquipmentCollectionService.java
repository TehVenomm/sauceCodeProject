package net.gogame.gowrap.ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.support.HttpException;
import net.gogame.gowrap.ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;

public interface EquipmentCollectionService {
    EquipmentCollectionResponse getEquipmentCollection(String str) throws IOException, HttpException;
}
