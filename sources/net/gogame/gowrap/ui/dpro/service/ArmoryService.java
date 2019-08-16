package net.gogame.gowrap.p019ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.p019ui.dpro.model.armory.ArmoryResponse;
import net.gogame.gowrap.support.HttpException;

/* renamed from: net.gogame.gowrap.ui.dpro.service.ArmoryService */
public interface ArmoryService {
    ArmoryResponse getArmory(String str) throws IOException, HttpException;
}
