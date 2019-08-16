package net.gogame.gowrap.p019ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.equipmentcollection.EquipmentCollectionResponse;
import net.gogame.gowrap.support.HttpException;

/* renamed from: net.gogame.gowrap.ui.dpro.service.EquipmentCollectionService */
public interface EquipmentCollectionService {
    EquipmentCollectionResponse getEquipmentCollection(String str) throws IOException, HttpException;
}
