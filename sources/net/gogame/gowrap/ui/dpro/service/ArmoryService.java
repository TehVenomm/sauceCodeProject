package net.gogame.gowrap.ui.dpro.service;

import java.io.IOException;
import net.gogame.gowrap.support.HttpException;
import net.gogame.gowrap.ui.dpro.model.armory.ArmoryResponse;

public interface ArmoryService {
    ArmoryResponse getArmory(String str) throws IOException, HttpException;
}
